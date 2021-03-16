using Autofac;
using Autofac.Core;
using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Database.Service;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Indexing.IOC;
using HomeVideo.Net.Library.Contracts;
using HomeVideo.Net.Library.IOC;
using HomeVideo.Net.Logging;
using HomeVideo.Net.Logging.Contracts;
using HomeVideo.Net.Services.Contracts;
using HomeVideo.Net.Services.Services;
using System;

namespace HomeVideo.Net.Host
{
    class Program
    {
        static IContainer _container;
        static void Main(string[] args)
        {
            //Container _container = new Container();

            /*
             * Example:
             * Program.cs:
             *   RepoLayerSession session = new Session();
             *       RepoLayerSession:
             *           Constructor:
             *               builder = new ContainerBuilder(); //AutoFac
             *               builder.RegisterInstances
             *                   new modules (i.e. new LibraryModule(), new IndexingModule())
             *               builder.RegisterModule(modules)
             *   IServices
             *   ILibraryService svc = session.GetService<T>();
             *   IIndexingService svc = session.GetService<T>();
             *   
             *   
             *   Order:
             *   DatabaseService
             *   LoggingService
             *   MetadataService
             *   
             *   Indexing
             *   Library
             *   
             *   
             *   
             *   Future:
             *   'multiplex'er that will handle file playback
             *   (Goal is to spin up a worker/service for each playback file (needing an instance of ffmpeg per?) So registration might look different for this piece
             */

            // Rough Draft Test
            string connectionString = @"J:\git\homevideo.net\test-data.db"; // TODO: use app.config / ConfigurationManager to store/retrieve these settings
            string apiKey = ""; 

            // Load specific base instances
            IDatabaseService dbInstance = new LiteDBService(connectionString);

            ILogger loggerInstance = new LiteDBLogger();

            IMetadataService metadataServiceInstance = new MetadataService(apiKey);

            // Load modules
            var builder = new ContainerBuilder();

            builder.RegisterInstance<IDatabaseService>(dbInstance);
            builder.RegisterInstance<ILogger>(loggerInstance);
            builder.RegisterInstance<IMetadataService>(metadataServiceInstance);
            builder.RegisterModule<IndexingModule>();
            builder.RegisterModule<LibraryModule>();

            _container = builder.Build();
            // Consider moving this all out to a supprting class loaded by program/servicebase

            var libraryService = _container.Resolve<ILibraryService>();

            var lib = libraryService.NewLibrary("test", @"L:\Movies", Domain.Enum.LibraryType.Movies);

            Console.WriteLine($"lib.id: {lib.Id}");

            Console.WriteLine($"Attemping library index...");
            var result = libraryService.IndexLibrary(lib.Id).GetAwaiter().GetResult();

            Console.WriteLine($"{result.Success}. {result.ResultCount}");

            Console.ReadKey();
        }
    }
}
