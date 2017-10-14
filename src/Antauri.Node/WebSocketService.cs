using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class WebSocketService
{
    private const int QUERY_LATEST = 0;
    private const int QUERY_ALL = 1;
    private const int RESPONSE_BLOCKCHAIN = 2;
    private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
 
    private readonly RequestDelegate _next;
 
    public WebSocketService(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task Invoke(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            await _next.Invoke(context);
            return;
        }
 
        CancellationToken ct = context.RequestAborted;
        WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
        var socketId = Guid.NewGuid().ToString();
 
        _sockets.TryAdd(socketId, currentSocket);
 
        while (true)
        {
            if (ct.IsCancellationRequested)
            {
                break;
            }
 
            var response = await currentSocket.ReceiveStringAsync(ct);
            if(string.IsNullOrEmpty(response))
            {
                if(currentSocket.State != WebSocketState.Open)
                {
                    break;
                }
 
                continue;
            }
 
            foreach (var socket in _sockets)
            {
                if(socket.Value.State != WebSocketState.Open)
                {
                    continue;
                }
 
                await socket.Value.SendStringAsync(response, ct);
            }
        }
 
        WebSocket dummy;
        _sockets.TryRemove(socketId, out dummy);
 
        await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
        currentSocket.Dispose();
    }
}