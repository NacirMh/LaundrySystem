using System.Net.WebSockets;

namespace LaundrySystem.WebApi.WebSocketLib
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

        public void RemoveSocket(WebSocket socket)
        {
            var session = _sockets.First(x => x.Socket == socket);
            _sockets.Remove(session);
        }
    }
}
