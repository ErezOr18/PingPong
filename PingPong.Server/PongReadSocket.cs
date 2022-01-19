using System;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using log4net;
using PingPong.Common;

namespace PingPong.Server
{
    public class PongReadSocket : IReadSocket<string>
    {
        private ILog _log;
        public PongReadSocket()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public string Read(IAsyncResult result)
        {
            String content = String.Empty;


            StateObject state = (StateObject)result.AsyncState;
            Socket handler = state.WorkSocket;

            int bytesRead = handler.EndReceive(result);


            state.MessageStringBuilder.Append(Encoding.ASCII.GetString(
                state.MessageBuffer, 0, bytesRead));


            content = state.MessageStringBuilder.ToString();

            _log.Info($"Received New Info from {handler}, info:{content}");
            return content;

        }
    }
}