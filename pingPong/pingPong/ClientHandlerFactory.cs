using pingPong.Common;
using pingPong.CoreAbstractions.BaseImpl;
using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;

namespace pingPong
{
    internal class ClientHandlerFactory : IClientHandlerFactory
    {
        private const int STRING_BUFFER_SIZE = 1024;
        public IClientHandler Create(ISocket socket)
        {
            return new ClientHandler(new PersonSocket(socket));
        }
    }
}
