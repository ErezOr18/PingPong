using pingPong.SocketsAbstractions;
using System.Net;

namespace pingPong.TcpImplementations
{
    public class TcpServerFactory : ServerListeningSocketFactoryBase
    {
        public override IServerListeningSocket Create(IPAddress address, int port)
        {
            return new TcpServerSocket(address, port);
        }
    }
}
