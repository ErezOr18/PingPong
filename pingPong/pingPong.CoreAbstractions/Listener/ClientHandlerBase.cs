
using log4net;
using pingPong.SocketsAbstractions;
using System.Reflection;
using System.Threading.Tasks;

namespace pingPong.CoreAbstractions.Listener
{
    public abstract class ClientHandlerBase<T>
    {
        protected readonly IObjectSocket<T> _socket;
        protected ILog _log;

        public ClientHandlerBase(IObjectSocket<T> socket)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);   
            _socket = socket;
        }

        
        public abstract Task HandleClient();
            
    }
}
