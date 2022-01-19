using System.Net.Sockets;
using PingPong.Common;

namespace PingPong.Server;
public class Server : System.IDisposable
{
    private Socket _listneningSocket;
    private ConnectionInfo _connectionInfo;

    public Server(ConnectionInfo connectionInfo,ProtocolType protocolType)
    {
        var addressFamily = connectionInfo.Address.AddressFamily
        _listneningSocket = new Socket(addressFamily,            SocketType.Stream, protocolType);
    }

    public void Dispose()
    {
        _listneningSocket.Dispose();
    }
}

