using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing;
using System;
using System.Collections.Generic;

namespace HomeVideo.Net.Library
{
    // 1 class, that has types, or movie library/tv library?
    public class MovieLibrary : ILibrary
    {
        public Guid Id { get; set; }
        public LibraryType LibraryType { get; set; }
        public string RootPath { get; set; }
        public string Name { get; set; }
        public List<IMovieData> LibraryItems { get; set; }
        public MovieIndexer Indexer { get; set; }
    }
}
