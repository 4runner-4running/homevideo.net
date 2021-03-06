using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Services.Contracts;
using MovieDB.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services.Services
{
    /// <summary>
    /// The metadata service is responsible for communicating with a chosen web api to retrieve information about movies and tv shows
    /// For now, the only option is themoviedb.org
    /// </summary>
    public class MetadataService : IMetadataService
    {
        MovieDBApi _client;
        public MetadataService()
        {
            _client = new MovieDBApi(""); //ToDo: where will the key info be located? need a storage service for config data
        }

        public IMovieData GetMovieByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public IMovieData GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public ITvData GetTvByTitle(string title)
        {
            throw new NotImplementedException();

        }
        public ITvData GetTvById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
