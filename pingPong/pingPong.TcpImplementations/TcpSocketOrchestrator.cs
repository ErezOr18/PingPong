using pingPong.SocketsAbstractions;
using System.Net;
using System.Net.Sockets;

namespace pingPong.TcpImplementations
{
    public class TcpSocketOrchestrator : ISocketOrchestraor
    {
        public ISocket Connect(IPAddress addr, int port)
        {
            var client = new TcpClient();
            client.Connect(new IPEndPoint(addr, port));
            return new TcpSocket(client);
        }
    }
}
