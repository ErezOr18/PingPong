
using pingPong.SocketsAbstractions;
using System.Net;
using System.Net.Sockets;

namespace pingPong.TcpImplementations
{
    public class TcpServerSocket : IServerListeningSocket
    {
        private readonly TcpListener _server;
        private readonly IPAddress _addr;
        private readonly int _port;

        public TcpServerSocket(IPAddress addr, int port)
        {
            _addr = addr;
            _port = port;
            _server = new TcpListener(addr, port);
        }

        public ISocket AcceptClient()
        {
            var client = _server.AcceptTcpClient();

            return new TcpSocket(client);
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
