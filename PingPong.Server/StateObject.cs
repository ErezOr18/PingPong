using System;
using System.Net.Sockets;
using System.Text;

namespace PingPong.Server
{
    public class StateObject
    {

        public const int BufferSize = 1024;

        public byte[] MessageBuffer;

        public StringBuilder MessageStringBuilder;

        public Socket WorkSocket;

        public StateObject()
        {
            MessageBuffer = new byte[BufferSize];
            MessageStringBuilder = new StringBuilder();
            WorkSocket = null;
        }

    }

}

