
namespace pingPong.CoreAbstractions.Listener
{
    public interface IListenerFactory
    {
        public IListener Create(int port);
    }
}
