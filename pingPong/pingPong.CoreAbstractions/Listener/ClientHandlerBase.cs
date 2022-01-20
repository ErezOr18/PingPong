
using log4net;
using pingPong.SocketsAbstractions;
using System.Reflection;
using System.Threading.Tasks;

namespace pingPong.CoreAbstractions.Listener
{
    public abstract class ClientHandlerBase
    {
        protected ILog _log;

        public ClientHandlerBase()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }


        public abstract Task HandleClient(ISocket socket);

    }
}
