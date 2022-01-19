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

        private ManualResetEvent _allDone;
        private IPEndPoint _connectionInfo;
        private Socket _listener;
        private ILog _log;
        private ISendSocket _sendSocket;
        private IReadSocket<string> _readSocket;

        public Server(IPEndPoint connectionInfo, ISendSocket sendSocket, IReadSocket<string> readSocket)
        {
            _allDone = new ManualResetEvent(false);
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _connectionInfo = connectionInfo;
            _sendSocket = sendSocket;
            _readSocket = readSocket;
        }

        public async Task StartListening()
        {

            _listener = new Socket(_connectionInfo.Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _listener.Bind(_connectionInfo);
                _listener.Listen();

                await Task.Run(() =>
                {
                    while (true)
                    {
                        _allDone.Reset();
                        _log.Info("waiting for new connection...");
                        _listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            _listener);
                        _allDone.WaitOne();
                    }
                });

            }
            catch (Exception e)
            {
                _log.Error(e);
            }

        }

        private void AcceptCallback(IAsyncResult ar)
        {

            _allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            _log.Info($"Accepted New Client {handler}");

            StateObject state = new StateObject();
            state.WorkSocket = handler;
            handler.BeginReceive(state.MessageBuffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var content = _readSocket.Read(ar);
            Send(((StateObject) ar.AsyncState).WorkSocket, content);
        }

        private void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                _sendSocket.Send(ar);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        public void Dispose()
        {
            _listener?.Dispose();
        }
    }
}