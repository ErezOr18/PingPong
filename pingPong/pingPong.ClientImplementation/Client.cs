using log4net;
using pingPong.CoreAbstractions.BaseImpl;
using pingPong.SocketsAbstractions;
using System;
using System.Net;
using System.Reflection;

namespace pingPong.ClientImplementation
{
    internal class Client
    {
        private readonly ISocketConnector _socketConnector;
        private readonly ILog _logger;

        public Client(ISocketConnector socketConnector)
        {
            _socketConnector = socketConnector;
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Run(string ip, int port)
        {
            _logger.Debug($"Connecting to {ip}:{port}");
            var socket = _socketConnector.Connect(IPAddress.Parse(ip), port);
            var stringSocket = new StringSocket(socket, 1024);
            _logger.Debug("Connected");
            string msg;
            do
            {
                Console.WriteLine("Enter Message:");
                msg = Console.ReadLine();

                stringSocket.Send(msg);
                var received = stringSocket.Receive();
                Console.WriteLine($"Server Replied {received}");
            } while (msg != "stop");
            _logger.Debug("Ended");
        }
    }
}
