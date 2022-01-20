using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;

namespace pingPong
{
    public class ClientHandler : IClientHandler
    {
        private readonly IObjectSocket<string> _socket;

        public ClientHandler(IObjectSocket<string> socket)
        {
            _socket = socket;
        }

        public void HandleClient()
        {
            string value;
            while ((value = _socket.Receive()).Length > 0)
            {
                _socket.Send(value);
            }
        }
    }
}
