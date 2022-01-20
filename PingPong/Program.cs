using log4net.Config;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            XmlConfigurator.Configure();
            int port;
            if (args.Length > 0)
            {
                port = int.Parse(args[0]);
            }
            else
            {
                port = 11000;
            }
            var read = new PingPong.Server.PongReadSocket();
            var send = new PingPong.Server.PongSendSocket();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            var server = new PingPong.Server.Server(localEndPoint, send, read);
            var serverTask = server.StartListening();
            var client = new PingPong.Client.SocketClient(localEndPoint);
            while (true)
            {
                Console.WriteLine("Enter Input");
                var msg = Console.ReadLine();
                byte[] byteData = Encoding.ASCII.GetBytes(msg);
                client.SendMessge(byteData);
                client.TryReceive();
            }
            await serverTask;
        }
    }
}