using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Concurrent;

namespace HomeVideo.Net.Indexing
{
    /// <summary>
    /// Indexer Factory is responsible for managing indexers for various libraries
    /// Should indexers stay 'live' like this, or merely be config entries to spin up?
    /// </summary>
    public class IndexerFactory
    {
        private ConcurrentDictionary<Guid, IIndexer> _indexCache;

        private ILogger _logger;
        private IDatabaseService _db;

        public IndexerFactory(IDatabaseService db, ILogger logger)
        {
            _logger = logger;
            _db = db;
            _indexCache = new ConcurrentDictionary<Guid, IIndexer>();
        }

        public IIndexer BuildIndexer()
        {

        }

        public IIndexer GetIndexer(Guid id)
        {
            IIndexer indexer;
            if (_indexCache.ContainsKey(id))
            {
                _indexCache.TryGetValue(id, out indexer);
                return indexer;
            }


        }

    }
}
