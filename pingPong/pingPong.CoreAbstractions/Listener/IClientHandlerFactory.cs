using pingPong.SocketsAbstractions;

namespace pingPong.CoreAbstractions.Listener
{
    public interface IClientHandlerFactory
    {
        ClientHandlerBase Create(ISocket socket);
    }
}
