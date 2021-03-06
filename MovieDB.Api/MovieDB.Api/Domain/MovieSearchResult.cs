using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    /// <summary>
    /// Schema taken from:https://developers.themoviedb.org/3/search/search-movies example response
    /// </summary>
    public class MovieSearchResult
    {
        public string Poster_Path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public DateTime Release_Date { get; set; } // Keep as string?
        public int[] Genre_Ids { get; set; }
        public int Id { get; set; }
        public string Original_Title { get; set; }
        public string Original_Language { get; set; }
        public string Title { get; set; }
        public string  Backdrop_Path { get; set; }
        public decimal Popularity { get; set; }
        public int Vote_Count { get; set; }
        public bool Video { get; set; }
        public decimal Vote_Average { get; set; }
    }
}
