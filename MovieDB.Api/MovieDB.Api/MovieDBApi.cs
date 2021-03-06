using MovieDB.Api.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MovieDB.Api
{
    /// <summary>
    /// Wrapper class for convenience in dealing with themoviedb.org api
    /// This will be utilized by the indexers to get movie/tv show metadata
    /// </summary>
    public class MovieDBApi
    {
        private string _url = "https://api.themoviedb.org/3/";
        private string _apiKey = ""; // TODO: look into storing this elsewhere

        private HttpClient _client;

        public MovieDBApi(string key, string url = null)
        {
            _apiKey = key;
            _url = !String.IsNullOrEmpty(url) ? url : _url;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            // Set any needed headers authorizations etc. here
            // Add request header to accept json
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> GetConfiguration()
        {
            // https://api.themoviedb.org/3/configuration?api_key=<<api_key>>
            var urlRequest = $"{ApiConstants.Configuration}?{ApiConstants.ApiKey}{_apiKey}";

            var response = await _client.GetAsync(urlRequest);

            return true;
        }

        #region Movie Requests
        /// <summary>
        /// Provide a search term for a movie title and retrieve a list of results
        /// </summary>
        /// <param name="title">query string for movie title</param>
        /// <returns></returns>
        public async Task<MovieSearchApiResponse> SearchForMovie(string title)
        {
            // Build out request url, most be properly encoded
            // Format: https://api.themoviedb.org/3/search/movie?api_key=<<api-key>>&language=en-us&query=<<query_string>>&language=en-US&page=1&include_adult=false
            // For my purposes &page=1&include_adult=false will always be set at the end of the query (ApiConstants.DefaultSearchProps), no need to peruse adult titles, and will always want the best matches from page 1
            var urlRequest = $"{ApiConstants.Search}{ApiConstants.Movie}?{ApiConstants.ApiKey}{_apiKey}&{ApiConstants.Lang}&{ApiConstants.Query}{HttpUtility.UrlEncode(title, Encoding.UTF8)}&{ApiConstants.DefaultSearchProps}";

            // Make request
            var response = await _client.GetAsync(urlRequest);

            // Handle success/failure
            if (ParseStatusCodeForPassFail(response.StatusCode))
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                // Parse string content into result object
                return JsonConvert.DeserializeObject<MovieSearchApiResponse>(stringContent);
            }
            else
            {
                throw new HttpRequestException($"Error requesting search data. Status: {response.StatusCode}", null, response.StatusCode);
            }
        }

        /// <summary>
        /// Get a movie data object based on internal id
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <returns></returns>
        public async Task<MovieApiDTO> GetMovie(int id)
        {
            // Format: https://api.themoviedb.org/3/movie/<<id>>?api_key=<<api_key>>&language=en-US
            var urlRequest = $"{ApiConstants.Movie}/{id}?{ApiConstants.ApiKey}{_apiKey}&{ApiConstants.Lang}";

            // Request
            var response = await _client.GetAsync(urlRequest);

            // Handle success/failure
            if (ParseStatusCodeForPassFail(response.StatusCode))
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<MovieApiDTO>(stringContent);
            }
            else
            {
                throw new HttpRequestException($"Error requesting movie title. Status: {response.StatusCode}", null, response.StatusCode);
            }
        }
        #endregion

        #region TV Requests
        /// <summary>
        /// Prove a search term for a tv show title and retrieve a list of results
        /// </summary>
        /// <param name="title">query strin for tv show title</param>
        /// <returns></returns>
        public async Task<TvSearchApiResponse> SearchForTv(string title)
        {
            // Format: https://api.themoviedb.org/3/search/tv?api_key=<<api_key>>&query=<<query_string>>&language=en-US&page=1&include_adult=false
            var urlRequest = $"{ApiConstants.Search}{ApiConstants.Tv}?{ApiConstants.ApiKey}{_apiKey}&{ApiConstants.Lang}&{ApiConstants.Query}{HttpUtility.UrlEncode(title, Encoding.UTF8)}&{ApiConstants.DefaultSearchProps}";

            // Request
            var response = await _client.GetAsync(urlRequest);

            // Handle success/failure
            if (ParseStatusCodeForPassFail(response.StatusCode))
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                // Parse and return
                return JsonConvert.DeserializeObject<TvSearchApiResponse>(stringContent);
            }
            else
            {
                throw new HttpRequestException($"Error requesting search data. Status: {response.StatusCode}", null, response.StatusCode);
            }
        }

        /// <summary>
        /// Get a tv show data object based on internal id
        /// </summary>
        /// <param name="id">Tv show id</param>
        /// <returns></returns>
        public async Task<TvApiDTO> GetTvShow(int id)
        {
            // Format: https://api.themoviedb.org/3/tv/{tv_id}?api_key=<<api_key>>&language=en-US
            var urlRequest = $"{ApiConstants.Tv}/{id}?{ApiConstants.ApiKey}{_apiKey}&{ApiConstants.Lang}";

            // Request
            var response = await _client.GetAsync(urlRequest);

            // Handle success/failure
            if (ParseStatusCodeForPassFail(response.StatusCode))
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TvApiDTO>(stringContent);
            }
            else
            {
                throw new HttpRequestException($"Error requesting movie title. Status: {response.StatusCode}", null, response.StatusCode);
            }
        }
        #endregion

        #region Helpers
        public bool ParseStatusCodeForPassFail(HttpStatusCode status)
        {
            // The moviedb.org api returns either 200, 401, or 404
            switch (status)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                default:
                    return false;
            }
        }
        #endregion
    }
}
