using System.Net;

namespace pingPong.SocketsAbstractions
{
    public abstract class ServerListeningSocketFactoryBase
    {
        public abstract IServerListeningSocket Create(IPAddress address, int port);
    }
}
