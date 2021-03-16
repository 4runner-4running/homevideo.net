using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Library.Contracts
{
    public interface ILibraryFactory
    {
        IEnumerable<ILibrary> GetAllLibraries();
        ILibrary GetLibrary(Guid id);
        //ILibrary GetLibraryByName(string name);
        ILibrary BuildLibrary(string name, string path, LibraryType type);
    }
}
