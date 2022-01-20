using System.Net;

namespace pingPong.SocketsAbstractions
{
    public interface ServerListeningSocketBase
    {
        public IServerListeningSocket Create(IPAddress address, int port);
    }
}
