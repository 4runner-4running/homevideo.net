using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    public class TvSearchApiResponse
    {
        public int Page { get; set; }
        public List<TvSearchResult> Results { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
    }
}
