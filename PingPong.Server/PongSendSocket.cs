using System;
using System.Net.Sockets;
using System.Reflection;
using log4net;

namespace PingPong.Server
{
	public class PongSendSocket : ISendSocket
    {
        private ILog _log;
		public PongSendSocket()
		{
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}

        public void Send(IAsyncResult result)
        {
            Socket handler = (Socket)result.AsyncState;
            int bytesSent = handler.EndSend(result);
            _log.Info($"sent to {handler} {bytesSent} bytes");
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}

