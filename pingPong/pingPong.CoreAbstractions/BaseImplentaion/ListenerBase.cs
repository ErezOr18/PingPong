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
        private readonly ServerListeningSocketFactoryBase _serverSocketFactory;
        private readonly ILog _logger;

        public ListenerBase(int port, IClientHandlerFactory clientHandlerFactory, ServerListeningSocketFactoryBase serverSocketFactory)
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
                    _logger.Info($"New Client Connected {client}");
                    var handler = _clientHandlerFactory.Create(client);
                    Task.Run(async () =>
                    {
                        await handler.HandleClient();
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
