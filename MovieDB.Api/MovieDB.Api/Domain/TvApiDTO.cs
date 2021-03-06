using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api.Domain
{
    public class TvApiDTO
    {
        public string Backdrop_Path { get; set; }
        //created_by
        public int[] Episode_Run_Time { get; set; }
        public DateTime First_Air_Date { get; set; }
        public Genre[] Genres { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public bool In_Production { get; set; }
        public string[] Languages { get; set; }
        public DateTime Last_Air_Date { get; set; }
        public EpisodeApiDTO Last_Episode_To_Air {get;set;}
        public string Name { get; set; }
        public object[] Networks { get; set; }
        public int Number_Of_Episodes { get; set; }
        public int Number_Of_Seasons { get; set; }
        public string[] Origin_Country { get; set; }
        public string Original_Language { get; set; }
        public string Original_Name { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public string Poster_Path { get; set; }
        public object[] Production_Companies { get; set; }
        public object[] Production_Countries { get; set; }
        public SeasonApiDTO[] Seasons { get; set; }
        public object[] Spoken_Languages { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Type { get; set; }
        public decimal Vote_Average { get; set; }
        public int Vote_Count { get; set; }
    }
}
