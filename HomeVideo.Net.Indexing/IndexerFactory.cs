using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Logging.Contracts;
using System;
using System.Collections.Concurrent;
using HomeVideo.Net.Database.Contracts;
using System.Collections.Generic;
using HomeVideo.Net.Services.Contracts;

namespace HomeVideo.Net.Indexing
{
    /// <summary>
    /// Indexer Factory is responsible for managing indexers for various libraries
    /// Should indexers stay 'live' like this, or merely be config entries to spin up?
    /// </summary>
    public class IndexerFactory : IIndexerFactory
    {
        private ConcurrentDictionary<Guid, IIndexer> _indexCache;

        private ILogger _logger;
        private IDatabaseService _databaseService;
        private IMetadataService _metadataService;

        public IndexerFactory(IDatabaseService databaseService, IMetadataService metadataService, ILogger logger)
        {
            _logger = logger;
            _databaseService = databaseService;
            _metadataService = metadataService;
            _indexCache = new ConcurrentDictionary<Guid, IIndexer>();
        }

        public IIndexer BuildIndexer(string name, string path, LibraryType type)
        {
            switch (type)
            {
                case LibraryType.Movies:
                    return BuildMovieIndexerInternal(name, path);
                case LibraryType.TV:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IIndexer GetIndexer(Guid id)
        {
            IIndexer indexer;
            if (_indexCache.ContainsKey(id))
            {
                _indexCache.TryGetValue(id, out indexer);
                return indexer;
            }

            // Handle failure
            _logger.WriteWarning($"Failed to load indexer: {id}");
            return null; // Meh
        }

        public IEnumerable<IIndexer> GetAllIndexers()
        {
            throw new NotImplementedException();
        }

        private MovieIndexer BuildMovieIndexerInternal(string libraryName, string path)
        {
            return new MovieIndexer(_logger, _metadataService, _databaseService, libraryName, path);
        }
    }
}
