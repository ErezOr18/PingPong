using System;
namespace PingPong.Server
{
	public interface IReadSocket<out T>
	{
		public T Read(IAsyncResult result);
	}
}

