using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.DataObjects;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Indexing.Domain;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing
{
    public class MovieIndexer : IIndexer
    {
        private const string _searchPattern = "*.mkv|*.mp4|*.avi";
        public Guid Id { get; private set; }
        public string LibraryName { get; set; }
        public LibraryType Type { get; set; }
        public string RootPath { get; set; }

        //private IStorageService _db { get; set; }
        private ILogger _logger;
        private IMetadataService _mdService;
        private IDatabaseService _storageService;
        /// <summary>
        /// Movie Indexer is responsible for movie libraries. It will expect one media file per folder
        /// </summary>
        /// <param name="name">The library name of the indexer</param>
        /// <param name="path">The root file path for the indexer to search</param>
        /// <param name="id"></param>
        public MovieIndexer(ILogger logger, IMetadataService metadataService, IDatabaseService dbService, string name, string path, Guid id = new Guid()) //IStorageService db
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();

            Type = LibraryType.Movies;

            LibraryName = name;
            RootPath = path;
            _logger = logger;
            _mdService = metadataService;
            _storageService = dbService;
        }

        public async Task<IndexResult> Index()
        {
            DateTime start = DateTime.Now;
            var movieDataList = new ConcurrentBag<MovieData>();

            // Search RootPath for files
            List<string> files = GetFilesList().ToList();

            // Iterate through and fetch metadata from 
            // How will the api react to this
            // Too many requests at once could end up with a throttle...
            await Task.Run(() =>
            {
                Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (file) =>
                {
                    var movieData = await FetchMetadata(file);
                    movieDataList.Add(movieData);
                });
            });

#if !DEBUG
            foreach (var movie in movieDataList)
            {
                _storageService.SaveEntry<MovieData>(movie, true);
            }
#endif
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
        // TODO: Potential bug - GetFilesList seems to be returning more items than exist in the given path.
        //  Behavior indicates that it is returning all found items for each search pattern
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
                    temp.AddRange(Directory.GetFiles(RootPath, pattern, SearchOption.AllDirectories));
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
        /// This includes the title, overview, release date, and image data
        /// </summary>
        /// <param name="file">File path containing the movie name</param>
        /// <returns></returns>
        private async Task<MovieData> FetchMetadata(string file)
        {
            string title = FileUtility.FormatFileNameForSearch(file);

            MovieData dataResponse = (MovieData) await _mdService.GetMovieByTitle(title);

            // Get image bytes for poster, backdrop, and thumb
            dataResponse.ImageBytes = await _mdService.GetMovieImage(dataResponse.PosterPath);
            dataResponse.ThumbBytes = await _mdService.GetMovieThumb(dataResponse.PosterPath);
            dataResponse.BackdropBytes = await _mdService.GetMovieImage(dataResponse.BackdropPath);

            //Get movie poster to store as byte array
            return dataResponse;
            /*
             *  Id = Guid.NewGuid(),
                MovieDbId = dto.Id,
                DisplayName = dto.Original_Title,
                MetadataDescription = dto.Overview,
                ReleaseDate = dto.Release_Date
             */
        }
    }
}
