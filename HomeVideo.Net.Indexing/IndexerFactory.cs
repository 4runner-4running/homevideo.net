using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Logging.Contracts;
using System;
using System.Collections.Concurrent;

namespace HomeVideo.Net.Indexing
{
    /// <summary>
    /// Indexer Factory is responsible for managing indexers for various libraries
    /// </summary>
    public class IndexerFactory
    {
        private ConcurrentDictionary<Guid, IIndexer> _indexCache;

        private ILogger _logger;
        //private IStorageService _db;

        public IndexerFactory(/*IStorageService db,*/ ILogger logger)
        {
            _logger = logger;
            _indexCache = new ConcurrentDictionary<Guid, IIndexer>();
        }
    }
}
