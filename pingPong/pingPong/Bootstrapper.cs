using pingPong.Common;
using System.Net;
using TcpFramework.Server;
using TcpFramework.SocketImplentnaions.Tcp;

namespace pingPong
{
    internal class Bootstrapper
    {
        public Server Bootstrapp(string[] args)
        {
            var port = -1;
            if (args.Length == 1 && int.TryParse(args[0], out int recvedPort))
            {
                port = recvedPort;
            }
            else
            {
                System.Console.WriteLine("Usage: program.exe <port>");
                return default;
            }

            var serverFactory = new TcpServerFactory();

            var handlerFactory = new PersonClientHandlerFactory();
            var server = new Server(port, handlerFactory, serverFactory.Create(IPAddress.Any,port));
            return server;
        }
    }
}
