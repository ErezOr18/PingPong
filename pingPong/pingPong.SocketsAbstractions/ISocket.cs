
namespace pingPong.SocketsAbstractions
{
    public interface ISocket
    {
        public int Receive(byte[] buffer);

        public void Send(byte[] data);
        
        public void Close();
    }
}
