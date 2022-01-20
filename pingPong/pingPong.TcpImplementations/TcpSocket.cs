using pingPong.Common;
using pingPong.SocketsAbstractions;
using System.Net.Sockets;

namespace pingPong.TcpImplementations
{
    public class TcpSocket : ISocket
    {
        private readonly TcpClient _client;
        private readonly RequestInfoFormatter _requestInfoFormatter;

        public TcpSocket(TcpClient client)
        {
            _client = client;
            _requestInfoFormatter = new RequestInfoFormatter();
        }

        public void Close()
        {
            _client.GetStream().Close();
            _client.Close();
        }

        public int Receive(byte[] buffer)
        {
            return _client.GetStream().Read(buffer, 0, buffer.Length);
        }


        public void Send(RequestInfo requestInfo)
        {
            var data = _requestInfoFormatter.Serialize(requestInfo);
            _client.GetStream().Write(data, 0, data.Length);
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{_client.Client.RemoteEndPoint}";
        }
    }
}
