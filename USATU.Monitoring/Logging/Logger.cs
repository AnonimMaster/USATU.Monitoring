using System;
using System.IO;

namespace USATU.Monitoring.Web.Logging
{
    public class Logger: ILogger
    {
        private readonly string _fileName;

        public Logger(string fileName)
        {
            _fileName = fileName;
        }

        public void Log(Exception exception)
        {
            File.WriteAllTextAsync(_fileName, exception.Message);
        }

        public void Log(string message)
        {
            File.WriteAllTextAsync(_fileName, File.ReadAllText(_fileName) + message);
        }
    }
}