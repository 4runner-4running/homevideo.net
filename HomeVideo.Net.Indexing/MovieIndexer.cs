using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Indexing.Domain;
using HomeVideo.Net.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing
{
    public class MovieIndexer : IIndexer
    {
        private const string _searchPattern = "*.mkv|*.mp4|*.avi";
        public Guid Id { get; private set; }
        public string LibraryName { get; set; }
        public LibraryType Type { get; set; }
        public string Path { get; set; }

        //private IStorageService _db { get; set; }
        private ILogger _logger;

        /// <summary>
        /// Movie Indexer is responsible for movie libraries. It will expect one media file per folder
        /// </summary>
        /// <param name="name">The library name of the indexer</param>
        /// <param name="path">The root file path for the indexer to search</param>
        /// <param name="id"></param>
        public MovieIndexer(ILogger logger, string name, string path, Guid id = new Guid()) //IStorageService db
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();

            Type = LibraryType.Movies;

            LibraryName = name;
            Path = path;
            _logger = logger;
            //_db = db;
        }

        public async Task<IndexResult> Index()
        {
            DateTime start = DateTime.Now;
            List<string> files = GetFilesList().ToList();
            DateTime stop = DateTime.Now;

            return new IndexResult()
            {
                Duration = stop - start,
                ResultCount = files.Count,
                Success = true,
                Messages = new List<string>()
            };

            /*
             * Outline:
             * 
             * The Index action will collect the fileinfo data of a directory path, recursing all folders
             * For each returned instance it needs to ping a movie database for metadata, and build out a litedb entry
             * Considerations: spin up multiple threads (can multi-write to litedb?) Parallel.ForEach
             */
        }

        // Fun chance to use queue and yield, taken from:
        // https://stackoverflow.com/questions/2106877/is-there-a-faster-way-than-this-to-find-all-the-files-in-a-directory-and-all-sub
        private IEnumerable<string> GetFilesList()
        {
            Queue<string> pending = new Queue<string>();
            var patterns = _searchPattern.Split("|");
            foreach (var pattern in patterns)
            {
                pending.Enqueue(pattern);
            }

            List<string> temp = new List<string>();

            while (pending.Count > 0)
            {
                var pattern = pending.Dequeue();

                try
                {
                    temp.AddRange(Directory.GetFiles(Path, pattern, SearchOption.AllDirectories));
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.WriteError($"", ex);
                    continue;
                }

                for (var i = 0; i < temp.Count; i++)
                {
                    yield return temp[i];
                }
            }
        }

        /*
         * Firstly,
         * load the movies db file, and get a list of all existing entries to compare against.
         * Load new files from library path
         * Compare results
         * index new entries by:
         *  looking up info against a relevant web api (imdb, moviedb.org, something else?)
         *  create db entry
         *  TODO: break data object up into tables?
         */

        /// <summary>
        /// Retrieve movie data from the movie db api service
        /// </summary>
        /// <param name="file">File path containing the movie name</param>
        /// <returns></returns>
        private Task<bool> FetchOnlineData(string file)
        {
            throw new NotImplementedException();
        }
    }
}
