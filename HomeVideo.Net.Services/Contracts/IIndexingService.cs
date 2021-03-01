using HomeVideo.Net.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services.Contracts
{
    public interface IIndexingService : IService
    {
       string LibraryPath { get; }
       LibraryType Type { get; set; }
    }
}
