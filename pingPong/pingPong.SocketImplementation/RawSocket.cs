using pingPong.Common;
using pingPong.SocketsAbstractions;
using System.Net.Sockets;

namespace pingPong.SocketImplementation
{
    public class RawSocket : ISocket
    {
        private readonly Socket _client;
        private readonly RequestInfoFormatter _requestInfoFormatter;
        public RawSocket(Socket client)
        {
            _client = client;
            _requestInfoFormatter = new RequestInfoFormatter();
        }

        public void Close()
        {
            _client.Close();
        }

        public int Receive(byte[] buffer)
        {
            return _client.Receive(buffer);
        }

        public void Send(RequestInfo requestInfo)
        {

            _client.Send(_requestInfoFormatter.Serialize(requestInfo));
        }
    }
}
