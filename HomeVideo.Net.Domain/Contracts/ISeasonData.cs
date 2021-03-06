using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface ISeasonData
    {
        Guid Id { get; set; }
        string DisplayName { get; set; }
        int EpisodeCount { get; set; }
        string MetadataDescription { get; set; }
        List<IEpisodeData> Episodes { get; set; }
    }
}
