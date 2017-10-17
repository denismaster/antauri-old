using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Antauri.Core;
using Newtonsoft.Json;
using WebSocket = Antauri.Node.WebSocketWrapper;
namespace Antauri.Node
{
    public class PeerToPeerService
    {
        private const int QUERY_LATEST = 0;
        private const int QUERY_ALL = 1;
        private const int RESPONSE_BLOCKCHAIN = 2;

        private readonly BlockChain blockChain;

        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public PeerToPeerService(BlockChain blockChain)
        {
            this.blockChain = blockChain ?? throw new System.ArgumentNullException(nameof(blockChain));
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

        private Task Write(System.Net.WebSockets.WebSocket socket, string message) => socket.SendStringAsync(message);

        public void ConnectToPeer(string peer)
        {
            var socket = WebSocketWrapper.Create(peer);
            socket.OnMessage(async (message, ws)=>{
                await HandleMessage(ws, message);
            });
            socket.OnConnect((ws)=>{
                Console.WriteLine($"Conection established with {peer}");
                _sockets.TryAdd(peer,ws);
            });
            socket.OnDisconnect((ws)=>{
                Console.WriteLine("Connection failed");
                _sockets.TryRemove(peer, out WebSocket s);
            });
            socket.Connect();
        }

        public async Task HandleMessage(WebSocket socket, string s)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<Message>(s);
                Console.WriteLine("Received message" + JsonConvert.SerializeObject(message));
                switch (message.Type)
                {
                    case QUERY_LATEST:
                        await Write(socket, ResponseLatestMessage());
                        break;
                    case QUERY_ALL:
                        await Write(socket, ResponseChainMessage());
                        break;
                    case RESPONSE_BLOCKCHAIN:
                        await handleBlockChainResponse(message.Data);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("hanle message is error:" + e.Message);
            }
        }

        public async Task HandleMessage(System.Net.WebSockets.WebSocket socket, string s)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<Message>(s);
                Console.WriteLine("Received message" + JsonConvert.SerializeObject(message));
                switch (message.Type)
                {
                    case QUERY_LATEST:
                        await Write(socket, ResponseLatestMessage());
                        break;
                    case QUERY_ALL:
                        await Write(socket, ResponseChainMessage());
                        break;
                    case RESPONSE_BLOCKCHAIN:
                        await handleBlockChainResponse(message.Data);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("hanle message is error:" + e.Message);
            }
        }

        private async Task handleBlockChainResponse(string message)
        {
            var receiveBlocks = JsonConvert.DeserializeObject<List<Block>>(message);
            receiveBlocks.OrderBy(block => block.Index);

            Block latestBlockReceived = receiveBlocks.Last();
            Block latestBlock = blockChain.LatestBlock;

            if (latestBlockReceived.Index > latestBlock.Index)
            {
                if (latestBlock.Hash == latestBlockReceived.PreviousHash)
                {
                    Console.WriteLine("We can append the received block to our chain");
                    blockChain.Add(latestBlockReceived);
                    await Broadcast(ResponseLatestMessage());
                }
                else if (receiveBlocks.Count == 1)
                {
                    Console.WriteLine("We have to query the chain from our peer");
                    await Broadcast(QueryAllMessage());
                }
                else
                {
                    blockChain.ReplaceChain(receiveBlocks);
                }
            }
            else
            {
                Console.WriteLine("received blockchain is not longer than received blockchain. Do nothing");
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
            Block[] blocks = { blockChain.LatestBlock };

            return JsonConvert.SerializeObject(new Message(RESPONSE_BLOCKCHAIN, JsonConvert.SerializeObject(blocks)));
        }
    }
}
