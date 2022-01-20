
using System.Threading.Tasks;

namespace pingPong.CoreAbstractions.Listener
{
    public interface IClientHandler
    {
        public Task HandleClient();
    }
}
