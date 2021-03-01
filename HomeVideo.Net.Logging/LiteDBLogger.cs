using HomeVideo.Net.Logging.Contracts;
using LiteDB;
using System;

namespace HomeVideo.Net.Logging
{
    public class LiteDBLogger : ILogger
    {
        private readonly string _databaseConnection = $@"{Environment.SpecialFolder.ApplicationData}\homevideo-logs.db";
        //Todo: connetion info
        public LiteDBLogger()
        {
        }
        public void WriteError(string messge, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void WriteInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteLog(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteWarning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
