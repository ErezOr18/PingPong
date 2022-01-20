using pingPong.SocketsAbstractions;

namespace pingPong.CoreAbstractions.Listener
{
    public interface IClientHandlerFactory<T>
    {
        ClientHandlerBase<T> Create(ISocket socket);
    }
}
