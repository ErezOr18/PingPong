using log4net;
using pingPong.Common;
using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace pingPong
{
    public class PersonClientHandler : ClientHandlerBase<Person>
    {

        public PersonClientHandler(PersonSocket socket) : base(socket)
        {
        }

        public override async Task HandleClient()
        {
            try
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
                            _log.Debug("waiting for client to send not null value");
                            await Task.Delay(200);
                        }
                        _log.Info($"send to client value of  {value}");
                        _socket.Send(value);
                    });
                    first = await Task.WhenAny(ttl, receive);
                } while (first != ttl);
                _log.Warn("client not responding");
            }
            catch (Exception e)
            {
                _log.Error(e);
                _socket.Close();

            }
        }
    }
}
