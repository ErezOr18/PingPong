using System.Net;

namespace pingPong.SocketsAbstractions
{
    public interface ISocketConnector
    {
        public ISocket Connect(IPAddress addr, int port);
    }
}
