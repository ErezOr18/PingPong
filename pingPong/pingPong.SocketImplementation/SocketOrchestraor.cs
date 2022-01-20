using pingPong.SocketsAbstractions;
using System.Net.Sockets;

namespace pingPong.SocketImplementation
{
    public class SocketOrchestraor : ISocket
    {
        private readonly Socket _client;

        public SocketOrchestraor(Socket client)
        {
            _client = client;
        }

        public void Close()
        {
            _client.Close();
        }

        public int Receive(byte[] buffer)
        {
            return _client.Receive(buffer);
        }

        public void Send(byte[] data)
        {
            _client.Send(data);
        }
    }
}
