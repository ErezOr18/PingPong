using log4net.Config;

namespace pingPong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrapp(args);
        }
    }
}
