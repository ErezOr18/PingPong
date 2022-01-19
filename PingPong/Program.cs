using System;

namespace PingPong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Erez");
            var server = new PingPong.Server.Server();
            server.StartListening();
        }
    }
}