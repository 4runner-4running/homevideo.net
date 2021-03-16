using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Library.Contracts;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Library
{
    public class LibraryFactory : ILibraryFactory
    {
        private ILogger _logger;
        private IDatabaseService _databaseService;
        private IMetadataService _metadataService;
        private IIndexerFactory _indexerFactory;

        private ConcurrentDictionary<Guid, ILibrary> _libraryCache;

        public LibraryFactory(IDatabaseService databaseService, IMetadataService metadataService, IIndexerFactory indexerFactory, ILogger logger)
        {
            _logger = logger;
            _databaseService = databaseService;
            _metadataService = metadataService;
            _indexerFactory = indexerFactory;

            _libraryCache = new ConcurrentDictionary<Guid, ILibrary>();

            LoadLibrariesIntoCache();
        }

        public void LoadLibrariesIntoCache()
        {
            List<ILibrary> libraries = _databaseService.GetEntries<ILibrary>("library_*");

            foreach(var library in libraries)
            {
                _libraryCache.TryAdd(library.Id, library);
            }
        }

        public IEnumerable<ILibrary> GetAllLibraries()
        {
            return _libraryCache.Values.ToList();
        }

        public ILibrary GetLibrary(Guid id)
        {
            ILibrary library;
            var success = _libraryCache.TryGetValue(id, out library);
            //TODO: consider null case
            return library;
        }

        public ILibrary BuildLibrary(string name, string path, LibraryType type)
        {
            switch (type)
            {
                case LibraryType.Movies:
                    return BuildMovieLibrary(name, path, _indexerFactory.BuildIndexer(name, path, type));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public MovieLibrary BuildMovieLibrary(string name, string path, IIndexer indexer)
        {
            // Create library
            var library = new MovieLibrary(name, path, indexer);

            // Add to cache
            // Don't really worry about the fail case for now, in this limited use case, there's not really a reason/way that we would duplicate library entries, short of GUID duplication...
            _libraryCache.TryAdd(library.Id, library);

            return library;
        }
    }
}
