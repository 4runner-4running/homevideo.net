using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Database.Contracts
{
    public interface IDatabaseService
    {
        bool SaveEntry<T>(T entry, string collectionName = null, bool overwrite = false);
        T GetEntry<T>(Guid id, string collectionName = null);
        List<T> GetEntries<T>(string field, string value, string collectionName = null);
        List<T> GetAllEntries<T>(string collectionName = null);
        bool DeleteEntry<T>(Guid id, string collectionName = null);

        void CreateIndexes<T>(string collectionName, string[] fields);
    }
}
