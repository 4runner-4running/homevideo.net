using System;
using System.Collections.Generic;
using System.Text;

namespace HomeVideo.Net.Logging.Contracts
{
    public interface ILogger
    {
        void WriteError(string messge, Exception ex);

        void WriteInfo(string message);

        void WriteLog(string message);

        void WriteWarning(string message);
    }
}
