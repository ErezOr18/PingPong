using pingPong.SocketsAbstractions;
using System.Net.Sockets;

namespace pingPong.TcpImplementations
{
    public class TcpSocket : ISocket
    {
        private readonly TcpClient _client;

        public TcpSocket(TcpClient client)
        {
            _client = client;
        }

        public int Receive(byte[] buffer)
        {
            return _client.GetStream().Read(buffer, 0, buffer.Length);
        }

        public void Send(byte[] data)
        {
            _client.GetStream().Write(data, 0, data.Length);
        }
    }
}
