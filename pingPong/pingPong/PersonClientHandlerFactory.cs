using pingPong.Common;
using pingPong.CoreAbstractions.BaseImpl;
using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;

namespace pingPong
{
    internal class PersonClientHandlerFactory : IClientHandlerFactory<Person>
    {
        public ClientHandlerBase<Person> Create(ISocket socket)
        {
            return new PersonClientHandler(new PersonSocket(socket));
        }
    }
}
