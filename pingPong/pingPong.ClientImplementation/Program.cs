using log4net.Config;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using TcpFramework.SocketImplentnaions.Tcp;

namespace pingPong.ClientImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var client = new Client(new TcpSocketOrchestrator(), new BinaryFormatter());
            client.Connect(new IPEndPoint(IPAddress.Parse(args[1]), int.Parse(args[0])));
            client.Run();
            client.Close();
        }
    }
}
