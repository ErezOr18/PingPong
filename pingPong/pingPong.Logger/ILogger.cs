using System;

namespace pingPong.Logger
{
    public interface ILogger
    {
        public void Debug(object log, Exception e = null);
        public void Info(object log, Exception e = null);
        public void Warn(object log, Exception e = null);
        public void Error(object log, Exception e = null);
        public void Fatal(object log, Exception e = null);
    }
}
