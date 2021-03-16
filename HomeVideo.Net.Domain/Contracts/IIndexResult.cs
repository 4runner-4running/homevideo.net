using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IIndexResult
    {
        bool Success { get; set; }
        List<string> Messages { get; set; }
        int ResultCount { get; set; }
        TimeSpan Duration { get; set; }
    }
}
