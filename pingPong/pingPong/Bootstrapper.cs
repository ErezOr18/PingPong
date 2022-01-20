using pingPong.CoreAbstractions.BaseImpl;
using pingPong.SocketImplementation;

namespace pingPong
{
    internal class Bootstrapper
    {
        public void Bootstrapp(string[] args)
        {
            var port = -1;
            if (args.Length == 1 && int.TryParse(args[0], out int recvedPort))
            {
                port = recvedPort;
            }
            else
            {
                System.Console.WriteLine("Usage: program.exe <port>");
                return;
            }

            var serverFactory = new SocketServerFactory();

            var handlerFactory = new ClientHandlerFactory();
            var listener = new ListenerBase(port, handlerFactory, serverFactory);
            listener.StartListening();
        }
    }
}
