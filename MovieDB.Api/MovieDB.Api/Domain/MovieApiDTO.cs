using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    public class MovieApiDTO
    {
        public bool Adult { get; set; }
        public string Backdrop_Path { get; set; }
        public object Belongs_To_Collection { get; set; }
        public int Budget { get; set; }
        public Genre[] Genres { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public string Imdb_Id { get; set; }
        public string Original_Language { get; set; }
        public string Original_Title { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        //production companies
        //production countries
        public DateTime Release_Date { get; set; }
        public int Revenue { get; set; }
        public int Runtime { get; set; }
        //spoken languages
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
}
