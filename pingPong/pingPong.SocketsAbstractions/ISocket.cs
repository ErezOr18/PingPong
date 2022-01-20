
namespace pingPong.SocketsAbstractions
{
    public interface ISocket
    {
        public int Receive(byte[] buffer);

        public void Send(RequestInfo requestInfo);

        public void Close();
    }
}
