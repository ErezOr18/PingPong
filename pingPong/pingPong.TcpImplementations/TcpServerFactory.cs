using pingPong.SocketsAbstractions;
using System.Net;

namespace pingPong.TcpImplementations
{
    public class TcpServerFactory : ServerListeningSocketBase
    {
        public IServerListeningSocket Create(IPAddress address, int port)
        {
            return new TcpServerSocket(address, port);
        }
    }
}
