using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    public class EpisodeApiDTO
    {
        public string Air_Date { get; set; }
        public int Episode_Number { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Production_Code { get; set; }
        public int Season_Number { get; set; }
        public string Still_Path { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
}
