
namespace pingPong.SocketsAbstractions
{
    public interface IObjectSocket<T>
    {
        public T Receive();

        public void Send(T value);

        public void Close();
    }
}
