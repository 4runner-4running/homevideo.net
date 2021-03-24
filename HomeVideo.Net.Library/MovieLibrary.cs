using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.DataObjects;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing;
using HomeVideo.Net.Indexing.Contracts;
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
        public List<IMovieData> LibraryItems { get; set; }
        public MovieIndexer Indexer { get; set; }

        public MovieLibrary(string name, string path, IIndexer indexer)
        {
            Id = Guid.NewGuid();
            Name = name;
            RootPath = path;
            LibraryItems = new List<IMovieData>();
            Indexer = indexer as MovieIndexer;
        }

        public async Task<IIndexResult> PerformIndex()
        {
            return await Indexer.Index();
        }

        public void SetLibraryItems(List<IMovieData> items)
        {
            LibraryItems = items;
        }
    }
}
