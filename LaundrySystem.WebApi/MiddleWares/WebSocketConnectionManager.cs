using System.Net.WebSockets;

namespace LaundrySystem.WebApi.MiddleWares
{
    public class WebSocketConnectionManager
    {
        private readonly List<WebSocketSession> _sockets;
        public WebSocketConnectionManager()
        {
            _sockets = new List<WebSocketSession>();
        }

        public List<WebSocketSession> getSockets()
        {
            return _sockets;
        }

        public void AddNewSocket(WebSocketSession session)
        {
            _sockets.Add(session);
        }

        public void RemoveSocket(WebSocketSession session)
        {
            _sockets.Remove(session);
        }
    }
}
