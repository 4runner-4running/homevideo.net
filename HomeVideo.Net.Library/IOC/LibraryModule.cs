using Autofac;
using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Library.Contracts;
using HomeVideo.Net.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace HomeVideo.Net.Library.IOC
{
    public class LibraryModule : Module
    {
        private ILogger _logger;
        private IDatabaseService _databaseService;
        private IIndexingService _indexingService;

        public LibraryModule() { }
        public LibraryModule(IDatabaseService databaseService, IIndexingService indexingService, ILogger logger)
        {
            _logger = logger;
            _databaseService = databaseService;
            _indexingService = indexingService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryFactory>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<LibraryService>().AsImplementedInterfaces().SingleInstance();

            base.Load(builder);
        }
    }
}
