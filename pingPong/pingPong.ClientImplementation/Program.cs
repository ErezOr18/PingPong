using pingPong.SocketImplementation;

namespace pingPong.ClientImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new Client(new SocketConnector());
            client.Run("127.0.0.1", 11001);
        }
    }
}
