using log4net;
using pingPong.Common;
using pingPong.SocketsAbstractions;
using System;
using System.Net;
using System.Reflection;

namespace pingPong.ClientImplementation
{
    internal class Client
    {
        private readonly ISocketOrchestraor _socketOrchestrator;
        private readonly ILog _logger;

        public Client(ISocketOrchestraor socketOrchestraor)
        {
            _socketOrchestrator = socketOrchestraor;
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Run(string ip, int port)
        {
            _logger.Info($"Connecting to {ip}:{port}");
            var socket = _socketOrchestrator.Connect(IPAddress.Parse(ip), port);
            var personSocket = new PersonSocket(socket);
            _logger.Debug("Connected");
            string msg;
            do
            {
                Console.WriteLine("Enter Name:");
                msg = Console.ReadLine();
                string name = msg;
                Console.WriteLine("Enter Age:");
                int age = int.Parse(Console.ReadLine());
                var person = new Person(name, age);
                personSocket.Send(person);
                var received = personSocket.Receive();
                Console.WriteLine($"Server Replied {received}");
            } while (msg != "stop");
            _logger.Info("Client has Ended connection");
        }
    }
}
