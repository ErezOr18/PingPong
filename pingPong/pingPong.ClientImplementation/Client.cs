using log4net;
using pingPong.Common;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using TcpFramework.Client;
using TcpFramework.Sockets;

namespace pingPong.ClientImplementation
{
    public class Client : ClientBase
    {
        public Client(ISocketOrchestraor socketOrchestrator, IFormatter formatter) : base(socketOrchestrator, formatter)
        {
        }


        public override void Run()
        {
            byte[] buffer = new byte[1024];
            MemoryStream stream = new MemoryStream();
            string msg;
            do
            {
                stream = new MemoryStream();
                Console.WriteLine("Enter Name:");
                msg = Console.ReadLine();
                string name = msg;
                Console.WriteLine("Enter Age:");
                int age = int.Parse(Console.ReadLine());   
                var person = new Person(name, age);
                _formatter.Serialize(stream, person);
                _socket.Send(stream.ToArray());
                _socket.Receive(buffer);
                stream = new MemoryStream(buffer);
                stream.Seek(0, SeekOrigin.Begin);
                var received = (Person)_formatter.Deserialize(stream);
                Console.WriteLine($"Server Replied {received}");
            } while (msg != "stop");
            _logger.Info("Client has Ended connection");
        }
    }
}
