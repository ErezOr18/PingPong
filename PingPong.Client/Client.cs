using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using PingPong.Common;
using log4net;
using System.Reflection;
using System.Collections.Generic;

namespace PingPong.Client
{

    public class Client : IDisposable
    {

        private IPEndPoint _server;


        private ILog _log;
        private TcpClient _client;
        private NetworkStream _stream;

        public Client(IPEndPoint server)
        {
            try
            {
                _server = server;
                _client = new TcpClient();
                _client.Connect(_server);
                _stream = _client.GetStream();
                _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }


        public void SendMessage(byte[] data)
        {
            _stream.Write(data, 0, data.Length);
        }

        public byte[] ReadMessage()
        {
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[256];
            try
            {
                while (_stream.DataAvailable)
                {
                    _stream.Read(buffer, 0, buffer.Length) ;
                    data.AddRange(buffer);
                    _log.Debug($"read from server another piece");
                }
                _log.Info($"received from server total bytes of {data.Count}");
                return data.ToArray();
            }
            catch(Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public void Dispose()
        {
            _stream.Close();
            _client.Close();
            _stream?.Dispose();
            _client.Dispose();
        }
    }
}