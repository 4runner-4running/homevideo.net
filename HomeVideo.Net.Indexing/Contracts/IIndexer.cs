using HomeVideo.Net.Domain.Enum;
using HomeVideo.Net.Indexing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing.Contracts
{
    /// <summary>
    /// Indexer Interface - All indexers will inherit from this type
    /// </summary>
    public interface  IIndexer
    {
        Guid Id { get; }
        string LibraryName { get; set; }
        LibraryType Type { get; set; }
        Task<MovieIndexResult> Index();

    }
}
