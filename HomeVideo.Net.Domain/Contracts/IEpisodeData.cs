using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IEpisodeData
    {
        Guid Id { get; set; }
        string DisplayName { get; set; }
        string MetadataDescription { get; set; }
        int EpisodeNumber { get; set; }
        string Path { get; set; }
        decimal Runtime { get; set; }
    }
}
