using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using PingPong.Common;
using log4net;
using System.Reflection;

namespace PingPong.Client
{

    public class SocketClient : IDisposable
    {

        private IPEndPoint _server;

        private ManualResetEvent connectDone;
        private ManualResetEvent sendDone;
        private ManualResetEvent receiveDone;
        private String response = String.Empty;
        private ILog _log;
        private Socket _client;

        public SocketClient(IPEndPoint server)
        {
            _server = server;
            connectDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
            receiveDone = new ManualResetEvent(false);
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void InitClient()
        {
            try
            {
                _client = new Socket(_server.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                _client.BeginConnect(_server,
                            new AsyncCallback(ConnectCallback), _client);
                connectDone.WaitOne();

            }
            catch (Exception e)
            {
                _log.Info(e);
            }
        }

        public void SendMessge(string data)
        {
            Send(data);
            sendDone.WaitOne();
        }

        public void TryReceive()
        {
            Receive();
            receiveDone.WaitOne();
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {

                _client.EndConnect(ar);

                _log.Info($"Socket connected to {_client.RemoteEndPoint}");
 
                connectDone.Set();
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        private void Receive()
        {
            try
            { 
                StateObject state = new StateObject();
                state.WorkSocket = _client; 
                _client.BeginReceive(state.MessageBuffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {

                var state = (StateObject)ar;
                int bytesRead = _client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.MessageStringBuilder.Append(Encoding.ASCII.GetString(state.MessageBuffer, 0, bytesRead));
 
                    _client.BeginReceive(state.MessageBuffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
 
                    if (state.MessageStringBuilder.Length > 1)
                    {
                        response = state.MessageStringBuilder.ToString();
                    }
                    
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        private void Send(String data)
        { 
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            _client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _client);
        }


        private void SendCallback(IAsyncResult ar)
        {
            try
            { 
                Socket client = (Socket)ar.AsyncState;
  
                int bytesSent = client.EndSend(ar);
                _log.Info($"Sent {bytesSent} bytes to server."); 
                sendDone.Set();
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        public void Dispose()
        {
            _client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }
    }
}