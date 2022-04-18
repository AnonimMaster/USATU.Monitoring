using System;

namespace USATU.Monitoring.Web.Logging
{
    public interface ILogger
    {
        public void Log(Exception exception);
        public void Log(string message);
    }
}