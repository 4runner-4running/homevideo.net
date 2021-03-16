using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IMovieLibrary : ILibrary
    {
        List<IMovieData> LibraryItems { get; set; }
    }
}
