using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Library.Contracts
{
    public interface ILibraryService
    {
        IEnumerable<ILibrary> GetLibraries();
        ILibrary NewLibrary(string name, string path, LibraryType type);
        ILibrary GetLibraryById(Guid id);
        //ILibrary GetLibraryByName(string name);
        Task<IIndexResult> IndexLibrary(Guid libraryId);
    }
}
