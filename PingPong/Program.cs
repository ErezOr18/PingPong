using System;
using System.Net;

namespace PingPong
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
            var server = new PingPong.Server.Server(localEndPoint,send,read);
            server.StartListening();
            PingPong.Client.AsynchronousClient.StartClient();
        }
    }
}