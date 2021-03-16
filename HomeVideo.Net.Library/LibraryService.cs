using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Library.Contracts;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Library
{
    /// <summary>
    /// Should be responsible for handling communication between web and libraries / backend index service
    /// </summary>
    public class LibraryService : ILibraryService
    {
        private ILogger _logger;
        private IIndexerFactory _indexerFactory;
        private IDatabaseService _databaseService;
        private ILibraryFactory _libraryFactory;

        public LibraryService(ILibraryFactory libraryFactory, IIndexerFactory indexerFactory, IDatabaseService databaseService, ILogger logger)
        {
            _libraryFactory = libraryFactory;
            _indexerFactory = indexerFactory;
            _databaseService = databaseService;
            _logger = logger;
        }

        public IEnumerable<ILibrary> GetLibraries()
        {
            return _libraryFactory.GetAllLibraries();
        }

        public ILibrary GetLibraryById(Guid id)
        {
            return _libraryFactory.GetLibrary(id);
        }

        public ILibrary NewLibrary(string name, string path, LibraryType type)
        {
            return _libraryFactory.BuildLibrary(name, path, type);
        }

        public async Task<IIndexResult> IndexLibrary(Guid libraryId)
        {
            // Get library
            //TODO: Should factory be the getter of existant libraries, or should this be service level, and factory literally just creates new ones
            var library = _libraryFactory.GetLibrary(libraryId);

            // Perform index
            IIndexResult indexResult = await library.PerformIndex();

            // Return result
            return indexResult;
        }
    }
}
