using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services.Contracts
{
    public interface IDatabaseService
    {
        bool SaveEntry<T>(T dataObject);
        T GetEntry<T>(Guid id);
        List<T> GetEntries<T>(string searchPattern);
        bool DeleteEntry(Guid id);
    }
}
