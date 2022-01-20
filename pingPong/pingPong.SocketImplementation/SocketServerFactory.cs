using pingPong.SocketsAbstractions;
using System.Net;

namespace pingPong.SocketImplementation
{
    public class SocketServerFactory : ServerListeningSocketBase
    {
        public IServerListeningSocket Create(IPAddress address, int port)
        {
            return new SocketServer(address, port);
        }
    }
}
