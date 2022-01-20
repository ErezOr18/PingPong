using System.Net;

namespace pingPong.SocketsAbstractions
{
    public interface ISocketOrchestraor
    {
        public ISocket Connect(IPAddress addr, int port);
    }
}
