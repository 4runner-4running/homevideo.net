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

        private string _collectionKey { get { return LibraryName.ToLower().Replace(' ', '_') + "__" + Id.ToString().Split('-')[0]; } }
        private ILogger _logger;
        private IMetadataService _mdService;
        private IDatabaseService _storageService;
        /// <summary>
        /// Movie Indexer is responsible for movie libraries. It will expect one media file per folder
        /// </summary>
        /// <param name="name">The library name of the indexer</param>
        /// <param name="path">The root file path for the indexer to search</param>
        /// <param name="id"></param>
        public MovieIndexer(ILogger logger, IMetadataService metadataService, IDatabaseService dbService, string name, string path, Guid id = new Guid())
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();

            Type = LibraryType.Movies;

            LibraryName = name;
            RootPath = path;
            _logger = logger;
            _mdService = metadataService;
            _storageService = dbService;
        }

        public MovieIndexer()
        {

        }

        public async Task<MovieIndexResult> Index()
        {
            DateTime start = DateTime.Now;
            var movieDataList = new ConcurrentBag<MovieData>();

            // Search RootPath for files
            List<string> files = GetFilesList();

            // Iterate through and fetch metadata from 
            // How will the api react to this
            // Too many requests at once could end up with a throttle...

            await ForEachAsync<string, MovieData>(files, FetchMetadata, (movieData) =>
            {
                movieDataList.Add(movieData);
                _storageService.SaveEntry<MovieData>(movieData, _collectionKey, true);
            });

#if !DEBUG
            foreach (var movie in movieDataList)
            {
                _storageService.SaveEntry<MovieData>(movie,_collectionKey, true);
            }
#endif
            DateTime stop = DateTime.Now;
            
            return new MovieIndexResult()
            {
                Duration = stop - start,
                ResultCount = files.Count,
                Success = true,
                Items = movieDataList.ToList(),
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

        private List<string> GetFilesList()
        {
            var temp = new List<string>();

            try
            {
                temp.AddRange(Directory.EnumerateFiles(RootPath, "*", SearchOption.AllDirectories)
                                .Where(f => f.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                        f.EndsWith(".mkv", StringComparison.OrdinalIgnoreCase) ||
                                        f.EndsWith(".avi", StringComparison.OrdinalIgnoreCase)));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.WriteError(ex.Message, ex);
                return temp;
            }

            return temp;
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

            // Set path
            dataResponse.Path = file;
            dataResponse.DateAdded = DateTime.Now;
            // Get image bytes for poster, backdrop, and thumb
            dataResponse.ImageBytes = await _mdService.GetMovieImage(dataResponse.PosterPath);
            dataResponse.ThumbBytes = await _mdService.GetMovieThumb(dataResponse.PosterPath);
            dataResponse.BackdropBytes = await _mdService.GetMovieImage(dataResponse.BackdropPath);

            //Get movie poster to store as byte array
            return dataResponse;
        }

        // TODO: Found this info on dealing with throttled parallel.
        // De-genericizing for now, but obviously when the time comes to do tv as well, it can be extracted into a utility and used by both.
        private async Task ForEachAsync<TSource, TResult>(
            IEnumerable<TSource> source, Func<TSource, Task<TResult>> taskSelector, Action<TResult> resultProcessor)
        {
            var throttle = new SemaphoreSlim(initialCount: 1, maxCount: 4);

           await Task.WhenAll(from item in source
                                select ProcessAsync(item, taskSelector, resultProcessor, throttle));
        }

        private async Task ProcessAsync<TSource, TResult>(TSource item, Func<TSource, Task<TResult>> taskSelector,
                                        Action<TResult> resultProcessor, SemaphoreSlim throttle)
        {
            TResult result = await taskSelector(item);

            await throttle.WaitAsync();
            try
            {
                resultProcessor(result);
            }
            finally
            {
                throttle.Release();
            }
        }
    }
}
