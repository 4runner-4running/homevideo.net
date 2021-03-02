using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services
{
    public class IndexingService : IIndexingService
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Running { get; set; }
        public LibraryType Type { get; set; }
        public ServiceStatus Status { get; set; }
        public string LibraryPath { get; set; }

        public IndexingService()
        {

        }
    }
}
