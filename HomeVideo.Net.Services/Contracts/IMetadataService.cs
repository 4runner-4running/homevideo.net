using HomeVideo.Net.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services.Contracts
{
    /// <summary>
    /// Describes a service for retrieving movie and tv information from the internet
    /// </summary>
    public interface IMetadataService
    {
        Task<IMovieData> GetMovieByTitle(string title);
        Task<IMovieData> GetMovieById(int id);
        Task<ITvData> GetTvByTitle(string title);
        Task<ITvData> GetTvById(int id);
    }
}
