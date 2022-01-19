using System;
namespace PingPong.Server
{
	public interface ISendSocket
	{
		public void Send(IAsyncResult result);
	}
}

