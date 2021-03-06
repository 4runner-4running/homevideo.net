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
        IMovieData GetMovieByTitle(string title);
        IMovieData GetMovieById(int id);
        ITvData GetTvByTitle(string title);
        ITvData GetTvById(int id);
    }
}
