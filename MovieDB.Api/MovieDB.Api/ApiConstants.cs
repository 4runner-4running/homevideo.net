using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDB.Api
{
    /// <summary>
    /// Magic strings for the api wrapper
    /// </summary>
    public static class ApiConstants
    {
        public static string ApiKey = "api_key=";
        public static string Search = "search/";
        public static string Movie = "movie/";
        public static string Tv = "tv/";
        public static string Season = "season/";
        public static string Episode = "episode/";
        public static string Genre = "genre/";
        public static string Lang = "language=en-US";
        public static string Query = "query=";
        public static string DefaultSearchProps = "page=1&include_adult=false";
    }
}
