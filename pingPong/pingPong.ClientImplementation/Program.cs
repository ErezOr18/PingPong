using log4net.Config;
using pingPong.SocketImplementation;

namespace pingPong.ClientImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var client = new Client(new TcpSocketOrchestrator());
            client.Run(args[1], int.Parse(args[0]));
        }
    }
}
