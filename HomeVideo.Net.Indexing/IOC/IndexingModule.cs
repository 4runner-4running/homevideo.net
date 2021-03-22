using Autofac;
using HomeVideo.Net.Database.Contracts;
using HomeVideo.Net.Indexing.Contracts;
using HomeVideo.Net.Indexing.Services;
using HomeVideo.Net.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace HomeVideo.Net.Indexing.IOC
{
    public class IndexingModule : Module
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _dbService;

        public IndexingModule() { }
        public IndexingModule(IDatabaseService dbService, ILogger logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IndexerFactory>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<IndexingService>().AsImplementedInterfaces().SingleInstance();

            base.Load(builder);
        }
    }
}
