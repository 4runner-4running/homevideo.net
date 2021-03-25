using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.DataObjects;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing;
using HomeVideo.Net.Indexing.Contracts;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeVideo.Net.Library
{
    // 1 class, that has types, or movie library/tv library?
    public class MovieLibrary : IMovieLibrary
    {
        public Guid Id { get; set; }
        public string RootPath { get; set; }
        public string Name { get; set; }
        public List<MovieData> LibraryItems { get; set; }
        public MovieIndexer Indexer { get; set; }

        public MovieLibrary(string name, string path, IIndexer indexer)
        {
            Id = Guid.NewGuid();
            Name = name;
            RootPath = path;
            LibraryItems = new List<MovieData>();
            Indexer = indexer as MovieIndexer;
        }

        public MovieLibrary()
        {

        }

        public async Task<IIndexResult> PerformIndex()
        {
            var indexResult = await Indexer.Index();

            SetLibraryItems(indexResult.Items);

            return indexResult;
        }

        public void SetLibraryItems(List<MovieData> items)
        {
            LibraryItems = items;
        }
    }
}
