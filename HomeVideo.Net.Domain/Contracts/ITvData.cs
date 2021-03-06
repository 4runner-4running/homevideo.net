using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface ITvData
    {
        Guid Id { get; set; }
        string DisplayName { get; set; }
        string MetadataDescription { get; set; }
        int NumberOfSeasons { get; set; }
        int TotalEpisodes { get; set; }
        List<ISeasonData> Seasons { get; set; }
    }
}
