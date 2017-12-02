using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Antauri.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Antauri.Node
{
    public class PeerToPeerService
    {
        private const int QUERY_LATEST = 0;
        private const int QUERY_ALL = 1;
        private const int RESPONSE_BLOCKCHAIN = 2;

        private readonly SimpleBlockChain blockChain;
        private readonly ILogger<PeerToPeerService> logger;
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        private static ConcurrentDictionary<string, WebSocketWrapper> _peers = new ConcurrentDictionary<string, WebSocketWrapper>();

        public PeerToPeerService(SimpleBlockChain blockChain, ILogger<PeerToPeerService> logger)
        {
            this.blockChain = blockChain ?? throw new System.ArgumentNullException(nameof(blockChain));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddPeer(WebSocket socket)
        {
            var socketId = Guid.NewGuid().ToString();
            _sockets.TryAdd(socketId, socket);
        }

        public async Task Broadcast(string message)
        {
            foreach (var socket in _sockets)
            {
                await this.Write(socket.Value, message);
            }
        }

        private Task Write(WebSocket socket, string message) => socket.SendStringAsync(message);

        public void ConnectToPeer(string peer)
        {
            var socket = WebSocketWrapper.Create(peer);
            socket.OnMessage(async (message, ws)=>{
                await HandleMessage(ws.WebSocket, message);
            });
            socket.OnConnect((ws)=>{
                logger.LogInformation($"Conection established with {peer}");
                _sockets.TryAdd(peer,ws.WebSocket);
                _peers.TryAdd(peer,ws);
            });
            socket.OnDisconnect((ws)=>{
                logger.LogWarning($"Conection failed with {peer}");
                _sockets.TryRemove(peer, out WebSocket s);
            });
            socket.Connect();
        }

        public async Task HandleMessage(WebSocket socket, string s)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<Message>(s);
                 logger.LogInformation("Received message" + JsonConvert.SerializeObject(message));
                switch (message.Type)
                {
                    case QUERY_LATEST:
                        await Write(socket, ResponseLatestMessage());
                        break;
                    case QUERY_ALL:
                        await Write(socket, ResponseChainMessage());
                        break;
                    case RESPONSE_BLOCKCHAIN:
                        await HandleBlockChainResponse(message.Data);
                        break;
                }
            }
            catch (Exception e)
            {
                logger.LogError("hanle message is error:" + e.Message);
            }
        }

        private async Task HandleBlockChainResponse(string message)
        {
            var receiveBlocks = JsonConvert.DeserializeObject<List<SimpleBlock>>(message);
            receiveBlocks.OrderBy(block => block.Header.Index);

            SimpleBlock latestBlockReceived = receiveBlocks.Last();
            SimpleBlock latestBlock = blockChain.LatestBlock;

            if (latestBlockReceived.Header.Index > latestBlock.Header.Index)
            {
                if (latestBlock.Hash == latestBlockReceived.Header.PreviousHash)
                {
                    logger.LogInformation("We can append the received block to our chain");
                    blockChain.Add(latestBlockReceived);
                    await Broadcast(ResponseLatestMessage());
                }
                else if (receiveBlocks.Count == 1)
                {
                    logger.LogInformation("We have to query the chain from our peer");
                    await Broadcast(QueryAllMessage());
                }
                else
                {
                    blockChain.ReplaceChain(receiveBlocks);
                }
            }
            else
            {
                logger.LogInformation("received blockchain is not longer than received blockchain. Do nothing");
            }
        }

        private String QueryAllMessage()
        {
            return JsonConvert.SerializeObject(new Message(QUERY_ALL));
        }

        private String QueryChainLengthMessage()
        {
            return JsonConvert.SerializeObject(new Message(QUERY_LATEST));
        }

        private string ResponseChainMessage()
        {
            var chain = JsonConvert.SerializeObject(blockChain.Blocks);
            var message = new Message(RESPONSE_BLOCKCHAIN, chain);
            return JsonConvert.SerializeObject(message);
        }

        public string ResponseLatestMessage()
        {
            SimpleBlock[] blocks = { blockChain.LatestBlock };

            return JsonConvert.SerializeObject(new Message(RESPONSE_BLOCKCHAIN, JsonConvert.SerializeObject(blocks)));
        }
    }
}
