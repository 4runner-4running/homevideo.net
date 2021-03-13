using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing.Services
{
    public class IndexingService : IIndexingService
    {
        public Guid Id { get; set; }
        public string Name => "indexing-service";

        private readonly IIndexerFactory _factory;
        private readonly ILogger _logger;
        private readonly IDatabaseService _databaseService;
        private readonly IMetadataService _metadataService;

        public IndexingService(IDatabaseService databaseService, IMetadataService metadataService, ILogger logger)
        {
            _databaseService = databaseService;
            _logger = logger;
            _metadataService = metadataService;

            _factory = new IndexerFactory(databaseService, metadataService, logger);
        }

        public IIndexer GetIndexer(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IIndexer> GetAllIndexers()
        {
            throw new NotImplementedException();
        }

        public IIndexer BuildIndexer(string name, string path, LibraryType type)
        {
            throw new NotImplementedException();
        }
    }
}
