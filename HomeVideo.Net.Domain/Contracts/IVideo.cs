using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IVideo
    {
        Guid Id { get; set; }
        string FileName { get; set; }
        string DisplayName { get; set; }
        string Metadata { get; set; }
        string IMDBLink { get; set; }
        bool Played { get; set; }
        //TODO: Keep bookmark here, or on a player service? (Use a timespan or some other marker?)
    }
}
