using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.DataObjects;
using HomeVideo.Net.Services.Contracts;
using MovieDB.Api;
using MovieDB.Api.Domain;
using System;
using System.Collections.Generic;
using System.IO;
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
        public MetadataService(string apiKey)
        {
            _client = new MovieDBApi(apiKey); //ToDo: where will the key info be located? need a storage service for config data
        }

        public async Task<IMovieData> GetMovieByTitle(string title)
        {
            var searchResult = await _client.SearchForMovie(title);

            if (searchResult.Results.Count > 0 && searchResult.Results.First() != null)
            {
                var dto = await _client.GetMovie(searchResult.Results.First().Id);
                // Consider if search doesn't match- it will pull bad data
                return ConvertApiToMovieData(dto);
            }

            return new MovieData();
        }
        public async Task<IMovieData> GetMovieById(int id)
        {
            var dto = await _client.GetMovie(id);

            return ConvertApiToMovieData(dto);
        }

        public async Task<byte[]> GetMovieImage(string path)
        {
            var imageStream = await _client.GetMovieImage(path);
            using (MemoryStream ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        public async Task<byte[]> GetMovieThumb(string path)
        {
            var imageStream = await _client.GetMovieImage(path, true);
            using (MemoryStream ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        public async Task<ITvData> GetTvByTitle(string title)
        {
            throw new NotImplementedException();

        }
        public async Task<ITvData> GetTvById(int id)
        {
            throw new NotImplementedException();
        }

        private MovieData ConvertApiToMovieData(MovieApiDTO dto)
        {
            return new MovieData
            {
                Id = Guid.NewGuid(),
                MovieDbId = dto.Id,
                DisplayName = dto.Original_Title,
                MetadataDescription = dto.Overview,
                ReleaseDate = dto.Release_Date,
                PosterPath = dto.Poster_Path,
                BackdropPath = dto.Backdrop_Path
            };
        }

        private TvData ConvertApiToTvData(TvApiDTO dto)
        {
            return new TvData
            {
                Id = Guid.NewGuid(),
                MovieDbId = dto.Id,
                DisplayName = dto.Original_Name,
                MetadataDescription = dto.Overview,
                Seasons = ConvertApiToSeasonData(dto.Seasons),
                TotalEpisodes = dto.Number_Of_Episodes,
                TotalSeasons = dto.Number_Of_Seasons
            };
        }

        private List<ISeasonData> ConvertApiToSeasonData(SeasonApiDTO[] dtos)
        {
            var ls = new List<ISeasonData>();

            for(var i = 0; i < dtos.Length; i++)
            {
                var season = new SeasonData
                {
                    Id = Guid.NewGuid(),
                    MovieDbId = dtos[i].Id,
                    MetadataDescription = dtos[i].Overview,
                    EpisodeCount = dtos[i].Episodes.Length
                }; // TODO: need a GetEpisode endpoint on the api client
            }

            return ls;
        }
    }
}
