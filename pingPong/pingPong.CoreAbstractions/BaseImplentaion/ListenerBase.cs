using log4net;
using pingPong.CoreAbstractions.Listener;
using pingPong.SocketsAbstractions;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace pingPong.CoreAbstractions.BaseImpl
{
    public class ListenerBase : IListener
    {
        private readonly int _port;
        private readonly IClientHandlerFactory _clientHandlerFactory;
        private readonly ServerListeningSocketBase _serverSocketFactory;
        private readonly ILog _logger;

        public ListenerBase(int port, IClientHandlerFactory clientHandlerFactory, ServerListeningSocketBase serverSocketFactory)
        {
            _port = port;
            _clientHandlerFactory = clientHandlerFactory;
            _serverSocketFactory = serverSocketFactory;
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void StartListening()
        {
            var server = _serverSocketFactory.Create(IPAddress.Any, _port);
            try
            {
                server.Start();
                while (true)
                {
                    _logger.Debug("Waiting For Client...");
                    var client = server.AcceptClient();
                    _logger.Debug("Client Connected");
                    var handler = _clientHandlerFactory.Create(client);
                    Task.Run(() =>
                    {
                        handler.HandleClient();
                    });
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error", e);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
