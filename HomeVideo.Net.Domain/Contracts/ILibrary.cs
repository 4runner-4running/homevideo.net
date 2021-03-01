using HomeVideo.Net.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface ILibrary
    {
        Guid Id { get; set; }
        LibraryType Type { get; set; }
        string Name { get; set; }
        string RootPath { get; set; }
    }
}
