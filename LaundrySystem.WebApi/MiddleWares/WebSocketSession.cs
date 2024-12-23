using System.Net.WebSockets;

namespace LaundrySystem.WebApi.MiddleWares
{
    public class WebSocketSession
    {

        public string Id { get; set; }
        public string Token { get; set; }
        public WebSocket Socket { get; set; }
        
    }
}
