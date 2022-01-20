using pingPong.SocketsAbstractions;
using System.Net;
using System.Net.Sockets;

namespace pingPong.SocketImplementation
{
    public class SocketServer : IServerListeningSocket
    {
        private readonly Socket _server;
        private readonly IPAddress _addr;
        private readonly int _port;

        public SocketServer(IPAddress addr, int port)
        {
            _addr = addr;
            _port = port;
            _server = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public ISocket AcceptClient()
        {
            var client = _server.Accept();
            return new RawSocket(client);
        }

        public void Start()
        {
            _server.Bind(new IPEndPoint(_addr, _port));
            _server.Listen();
        }

        public void Stop()
        {
            _server.Close();
        }
    }
}
