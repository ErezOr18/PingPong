using pingPong.Common;
using System.Runtime.Serialization.Formatters.Binary;
using TcpFramework.Server.Listener;
using TcpFramework.Sockets;

namespace pingPong
{
    internal class PersonClientHandlerFactory : IClientHandlerFactory
    {
        public ClientHandlerBase Create(ISocket socket)
        {
            return new PersonClientHandler(new BinaryFormatter(),socket);
        }
    }
}
