using HomeVideo.Net.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.DataObjects
{
    public class SeasonData : ISeasonData
    {
        public Guid Id { get; set; }
        public int MovieDbId { get; set; }
        public string DisplayName { get; set; } // Seasons don't really have names?
        public int EpisodeCount { get; set; }
        public string MetadataDescription { get; set; }
        public List<IEpisodeData> Episodes { get; set; }
    }
}
