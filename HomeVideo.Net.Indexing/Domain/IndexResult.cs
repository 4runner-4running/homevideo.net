using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing.Domain
{
    public class IndexResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public int ResultCount { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
