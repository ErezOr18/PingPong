using pingPong.SocketsAbstractions;
using System.Net;
using System.Net.Sockets;

namespace pingPong.SocketImplementation
{
    public class TcpSocketOrchestrator : ISocketOrchestraor
    {
        public ISocket Connect(IPAddress addr, int port)
        {
            var client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(addr, port));
            return new RawSocket(client);
        }
    }
}
