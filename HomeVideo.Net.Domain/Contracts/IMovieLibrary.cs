using HomeVideo.Net.Domain.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    public interface IMovieLibrary : ILibrary
    {
        List<MovieData> LibraryItems { get; set; }
    }
}
