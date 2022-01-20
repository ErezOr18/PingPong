using pingPong.SocketsAbstractions;
using System.Net;

namespace pingPong.SocketImplementation
{
    public class SocketServerFactory : ServerListeningSocketFactoryBase
    {
        public override IServerListeningSocket Create(IPAddress address, int port)
        {
            return new SocketServer(address, port);
        }
    }
}
