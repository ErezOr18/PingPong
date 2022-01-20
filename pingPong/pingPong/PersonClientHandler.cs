using pingPong.Common;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TcpFramework.Server.Listener;
using TcpFramework.Sockets;

namespace pingPong
{
    public class PersonClientHandler : ClientHandlerBase
    {
        public PersonClientHandler(IFormatter formatter, ISocket socket) : base(socket, formatter)
        {
        }

        public override async Task HandleClient()
        {
            try
            {
                Person value;
                Task ttl;
                Task first;
                byte[] data = new byte[1024];
                do
                {
                    ttl = Task.Delay(5000);
                    var receive = Task.Run(async () =>
                    {

                        do
                        {
                            _socket.Receive(data);
                            MemoryStream stream = new MemoryStream(data);
                            stream.Seek(0, SeekOrigin.Begin);
                            value = (Person)_formatter.Deserialize(stream);
                            _log.Debug("waiting for client to send not null value");
                            await Task.Delay(200);
                        } while (value == null);
                        _log.Info($"send to client value of  {value}");
                        MemoryStream memoryStream = new MemoryStream();
                        _formatter.Serialize(memoryStream, value);
                        _socket.Send(memoryStream.ToArray());
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
