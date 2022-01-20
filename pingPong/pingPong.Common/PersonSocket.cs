
using pingPong.SocketsAbstractions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace pingPong.Common
{
    public class PersonSocket : IObjectSocket<Person>
    {
        private readonly ISocket _socket;
        private BinaryFormatter _binaryFormatter;
        public PersonSocket(ISocket socket)
        {
            _socket = socket;
            _binaryFormatter = new BinaryFormatter();
        }

        public Person Receive()
        {
            var data = new byte[256];
            _socket.Receive(data);
            var memoryStream = new MemoryStream(data);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return _binaryFormatter.Deserialize(memoryStream) as Person;
        }

        public void Send(Person value)
        {
            var memoryStream = new MemoryStream();
            _binaryFormatter.Serialize(memoryStream, value);
            _socket.Send(memoryStream.ToArray());
        }
    }
}
