using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using System.Reflection;
using System.Threading.Tasks;
using PingPong.Common;
// State object for reading client data asynchronously  
namespace PingPong.Server
{
    public class Server : IDisposable
    {

        private IPEndPoint _connectionInfo;
        private TcpListener _listener;
        private ILog _log;
        private ISendSocket _sendSocket;
        private IReadSocket<string> _readSocket;

        public Server(IPEndPoint connectionInfo, ISendSocket sendSocket, IReadSocket<string> readSocket)
        {

            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _connectionInfo = connectionInfo;
            _sendSocket = sendSocket;
            _readSocket = readSocket;
            _listener = new TcpListener(_connectionInfo);

        }

        public async Task StartListening()
        {

            _listener.Start();  
            try
            {

                await Task.Run(() =>
                {
                    while (true)
                    {

                        _log.Info("waiting for new connection...");
                        var client = _listener.AcceptTcpClient();
                        _log.Info($"received new tcp client {client.Client.RemoteEndPoint}");
                        HandleNewClient(client); 
                    }
                });

            }
            catch (Exception e)
            {
                _log.Error(e);
            }

        }

        private void HandleNewClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            int i;
            byte[] bytes = new byte[256];

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                _log.Info($"received from {client.Client.RemoteEndPoint} data: {data}");
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                stream.Write(msg, 0, msg.Length);
                _log.Info($"send to {client.Client.RemoteEndPoint} {data}");
            }

            client.Close();
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}