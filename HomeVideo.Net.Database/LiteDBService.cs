using HomeVideo.Net.Database.Contracts;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Database.Service
{
    public class LiteDBService : IDatabaseService
    {
        private ConnectionString _connectionString;

        public LiteDBService(string connectionString)
        {
            _connectionString = new ConnectionString();
            _connectionString.Filename = connectionString;
            _connectionString.Upgrade = true;
        }
        public bool SaveEntry<T>(T entry, string collectionName = null, bool overwrite = false)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                ILiteCollection<T> collection = String.IsNullOrEmpty(collectionName) ? db.GetCollection<T>() : db.GetCollection<T>(collectionName);

                if (overwrite)
                    return collection.Upsert(entry);
                else
                {
                    var insert = collection.Insert(entry);
                    return !insert.IsNull;
                }
            }
        }

        public T GetEntry<T>(Guid id, string collectionName = null)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = String.IsNullOrEmpty(collectionName) ? db.GetCollection<T>() : db.GetCollection<T>(collectionName);
                return collection.FindById(id);
            }
        }

        public List<T> GetEntries<T>(string field, string value, string collectionName = null)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = String.IsNullOrEmpty(collectionName) ? db.GetCollection<T>() : db.GetCollection<T>(collectionName);

                if (collection.Count() > 0)
                {
                    var results = collection.Find(Query.Contains(field, value));
                    return results.ToList();
                }

                return new List<T>();
            }
        }

        public List<T> GetAllEntries<T>(string collectionName = null)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = string.IsNullOrEmpty(collectionName) ? db.GetCollection<T>() : db.GetCollection<T>(collectionName);

                return collection.FindAll().ToList();
            }
        }

        public bool DeleteEntry<T>(Guid id, string collectionName = null)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var collection = String.IsNullOrEmpty(collectionName) ? db.GetCollection<T>() : db.GetCollection<T>(collectionName);

                return collection.Delete(id);
            }
        }
    }
}
