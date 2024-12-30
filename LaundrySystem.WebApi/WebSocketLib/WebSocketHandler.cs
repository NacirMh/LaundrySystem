using LaundrySystem.Domain.ValueObjects;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using LaundrySystem.Domain.Dtos.Machine;
using Microsoft.AspNetCore.Http;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using System.Linq.Expressions;

namespace LaundrySystem.WebApi.WebSocketLib
{
    public class WebSocketHandler
    {

        private readonly WebSocketConnectionManager _conManager;
        private readonly IJWTTokenManager _tokenManager;
        private readonly IServiceProvider _serviceProvider;

        public WebSocketHandler(WebSocketConnectionManager conManager , IServiceProvider provider)
        {
            _serviceProvider = provider;
            _conManager = conManager;
            _tokenManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IJWTTokenManager>();
        }


        public async Task SendMessageAsync(WebSocket ws, WebSocketMessage message)
        {
            var messageString = JsonSerializer.Serialize(message);
            var byteArray = Encoding.UTF8.GetBytes(messageString);
            var session = _conManager.getSockets().FirstOrDefault(x => x.Socket == ws);
            if (session != null && _tokenManager.verifyToken(session.Token))
            {
                await ws.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task SendMessageToAllSessionsAsync(string sessionId, WebSocketMessage message)
        {

            var wsSessions = _conManager.getSockets().Where(x => x.Id == sessionId);

            var messageJson = JsonSerializer.Serialize(message);

            foreach (var session in wsSessions)
            {
                if (session.Socket.State == WebSocketState.Open && _tokenManager.verifyToken(session.Token))
                {
                    try
                    {
                        await session.Socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageJson)), WebSocketMessageType.Text, true, CancellationToken.None);
                    }catch(Exception e)
                    {
                        Console.WriteLine(e);
                        
                    }
                }
            }

        }

    }
}
