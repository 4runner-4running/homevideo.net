using HomeVideo.Net.Services.Contracts;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services
{
    public class LiteDBService : IDatabaseService
    {
        private string _connectionString = "";

        public LiteDBService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool SaveEntry<T>(T entry, bool overwrite = false)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                ILiteCollection<T> collection = db.GetCollection<T>();

                if (overwrite)
                    return collection.Upsert(entry);
                else
                {
                    var insert = collection.Insert(entry);
                    return !insert.IsNull;
                }
            }
        }

        public T GetEntry<T>(Guid id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<T>();
                return collection.FindById(id);
            }
        }

        public List<T> GetEntries<T>(string searchPattern)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<T>();

                var results = collection.Find(searchPattern);
                return results.ToList();
            }
        }

        public bool DeleteEntry<T>(Guid id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = db.GetCollection<T>();

                return collection.Delete(id);
            }
        }
    }
}
