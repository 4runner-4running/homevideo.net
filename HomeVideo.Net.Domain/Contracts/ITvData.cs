﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface ITvData : IMedia
    {
        int TotalSeasons { get; set; }
        int TotalEpisodes { get; set; }
        List<ISeasonData> Seasons { get; set; }
    }
}
