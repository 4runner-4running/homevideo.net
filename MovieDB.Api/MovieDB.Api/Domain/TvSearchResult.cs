using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    /// <summary>
    /// Schema taken from: https://developers.themoviedb.org/3/search/search-tv-shows example response
    /// </summary>
    public class TvSearchResult
    {
        public string Poster_Path { get; set; }
        public decimal Popularity { get; set; }
        public int Id { get; set; }
        public string Backdrop_Path { get; set; }
        public decimal Vote_Average { get; set; }
        public DateTime? First_Air_Date { get; set; }
        public string[] Origin_Country { get; set; }
        public string[] Genre_Ids { get; set; }
        public string Original_Language { get; set; }
        public int Vote_Count { get; set; }
        public string Name { get; set; }
        public string Original_Name { get; set; }
    }
}
