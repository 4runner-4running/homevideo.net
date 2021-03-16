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

        /// <summary>
        /// Use if you desire to store logs in a specific location
        /// </summary>
        /// <param name="databaseOverride"></param>
        public LiteDBLogger(string databaseOverride)
        {
            _databaseConnection = databaseOverride;
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
