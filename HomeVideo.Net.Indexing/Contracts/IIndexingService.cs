using HomeVideo.Net.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Indexing.Contracts
{
    public interface IIndexingService
    {
        string Name { get; }
        IIndexer GetIndexer(Guid id);
        IEnumerable<IIndexer> GetAllIndexers();
        IIndexer BuildIndexer(string name, string path, LibraryType type);
    }
}
