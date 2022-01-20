using pingPong.Common;
using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;
using System.Threading.Tasks;

namespace pingPong
{
    public class ClientHandler : IClientHandler
    {
        private readonly IObjectSocket<Person> _socket;

        public ClientHandler(IObjectSocket<Person> socket)
        {
            _socket = socket;
        }

        public async Task HandleClient()
        {
            Person value;
            Task ttl;
            Task first;
            do
            {
                ttl = Task.Delay(5000);
                var receive = Task.Run(async () =>
                {
                    while ((value = _socket.Receive()) == null)
                    {
                        System.Console.WriteLine("here");
                        await Task.Delay(200);
                    }
                    _socket.Send(value);
                });
                first = await Task.WhenAny(ttl, receive);
            } while (first != ttl);
        }
    }
}
