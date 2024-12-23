using LaundrySystem.Domain.Dtos.Owner;
using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Business.Services;
using LaundrySystem.WebApi.Presentation.Mappers;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace LaundrySystem.WebApi.MiddleWares
{
    public class WebSocketMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly IJWTTokenManager _tokenManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfigurationService _configurationService;
        public WebSocketMiddleWare(RequestDelegate requestDelegate, WebSocketConnectionManager connectionManager,IServiceProvider serviceProvider)
        {
            _requestDelegate = requestDelegate;
            _connectionManager = connectionManager;
            _serviceProvider = serviceProvider;
            _tokenManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IJWTTokenManager>();
            _configurationService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IConfigurationService>();

        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {

                WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleConnection(websocket);

            }
            else
            {
                await _requestDelegate(context);
            }
        }

        public async Task HandleConnection(WebSocket ws)
        {
            var buffer = new byte[4096];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    var authMessage = JsonSerializer.Deserialize<AuthMessage>(message, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (authMessage.Type == "authenticate" && _tokenManager.verifyToken(authMessage.Token))
                    {
                        var socketSession = new WebSocketSession
                        {
                            Id = authMessage.Id,
                            Socket = ws,
                            Token = authMessage.Token,
                        };
                        _connectionManager.AddNewSocket(socketSession);
                        
                        await SendMessageAsync(ws, _configurationService.GetConfigurations(authMessage.Id).ToOwnerDTO());
                    }
                    else
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unauthorized", CancellationToken.None);
                    }
                }
                
            }
        }

        private async Task SendMessageAsync(WebSocket ws, Object message)
        {
            var messageString = JsonSerializer.Serialize(message);
            var byteArray = Encoding.UTF8.GetBytes(messageString);
            await ws.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

}
