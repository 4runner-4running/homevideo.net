using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IMedia
    {
        Guid Id { get; set; }
        int MovieDbId { get; set; }
        string DisplayName { get; set; }
        string MetadataDescription { get; set; }
    }
}
