using HomeVideo.Net.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.DataObjects
{
    public class TvData : ITvData
    {
        public Guid Id { get; set; }
        public int MovieDbId { get; set; }
        public string DisplayName { get; set; }
        public string MetadataDescription { get; set; }
        public int TotalEpisodes { get; set; }
        public List<ISeasonData> Seasons { get; set; }
        public int TotalSeasons { get; set; }
    }
}
