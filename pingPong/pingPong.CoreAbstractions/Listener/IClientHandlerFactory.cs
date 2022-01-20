using pingPong.SocketsAbstractions;

namespace pingPong.CoreAbstractions.Listener
{
    public interface IClientHandlerFactory
    {
        IClientHandler Create(ISocket socket);
    }
}
