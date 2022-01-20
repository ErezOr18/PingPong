
namespace pingPong.Logger
{
    public class Logger
    {
        public ILogger GetLogger(string name)
        {
            return new Log4Net(name);
        }
    }
}
