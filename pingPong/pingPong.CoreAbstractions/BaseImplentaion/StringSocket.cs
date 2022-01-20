using pingPong.SocketsAbstractions;
using System.Text;

namespace pingPong.CoreAbstractions.BaseImpl
{
    public class StringSocket : IObjectSocket<string>
    {
        private readonly byte[] _buffer;
        private readonly Encoding _encoding;
        private readonly ISocket _socket;

        public StringSocket(ISocket socket, int bufferSize, Encoding encoding)
        {
            _encoding = encoding;
            _socket = socket;
            _buffer = new byte[bufferSize];
        }
        public StringSocket(ISocket socket, int bufferSize) : this(socket, bufferSize, Encoding.UTF8)
        {
        }

        public string Receive()
        {
            var bytesRead = _socket.Receive(_buffer);
            return _encoding.GetString(_buffer, 0, bytesRead);
        }

        public void Send(string value)
        {
            var asBytes = _encoding.GetBytes(value);
            _socket.Send(asBytes);
        }

        public void Close()
        {

            _socket.Close();
        }
    }
}
