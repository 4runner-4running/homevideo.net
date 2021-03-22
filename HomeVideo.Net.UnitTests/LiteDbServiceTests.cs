using HomeVideo.Net.Database.Service;
using HomeVideo.Net.Domain.Contracts;
using HomeVideo.Net.Domain.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.UnitTests
{
    [TestClass]
    public class LiteDbServiceTests
    {
        private readonly string _connectionString = @".\unit-testing.db";
        [TestMethod]
        public void Test_SaveEntry()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Testinator 3000" };

            // Act
            var result = db.SaveEntry<IMovieData>(entry);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_connectionString));

            // Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetEntry()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry = new MovieData { Id = Guid.NewGuid(), DisplayName = "Testinator 3001" };
            var id = entry.Id;

            // Act
            var result = db.SaveEntry<IMovieData>(entry);

            var record = (MovieData)db.GetEntry<IMovieData>(id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(entry.Id, record.Id);
            Assert.AreEqual(entry.DisplayName, record.DisplayName);
            Assert.IsTrue(File.Exists(_connectionString));

            // Clean up
            TearDownTestObjects();

        }

        [TestMethod]
        public void Test_SaveEntry_WithCollectionName()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Testinator 3002" };

            // Act
            var result = db.SaveEntry<IMovieData>(entry, "Movies");

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_connectionString));

            // Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetEntry_WithCollectionName()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Testinator 3003" };
            var id = entry.Id;
            var result = db.SaveEntry<IMovieData>(entry, "Movies");

            // Act
            var record = db.GetEntry<MovieData>(id, "Movies");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(entry.Id, record.Id);
            Assert.AreEqual(entry.DisplayName, record.DisplayName);
            Assert.IsTrue(File.Exists(_connectionString));

            // Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetEntries()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry1 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 1" };
            var entry2 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 2" };
            var entry3 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 3" };


            db.SaveEntry<IMovieData>(entry1, "Movies");
            db.SaveEntry<IMovieData>(entry2, "Movies");
            db.SaveEntry<IMovieData>(entry3, "Movies");

            // Act
            var results = db.GetEntries<IMovieData>("DisplayName", "Movie", "Movies");

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, 3);
            Assert.IsTrue(File.Exists(_connectionString));

            //  Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetAllEntries_NoCollectionName()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry1 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 1" };
            var entry2 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 2" };
            var entry3 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 3" };


            db.SaveEntry<IMovieData>(entry1);
            db.SaveEntry<IMovieData>(entry2);
            db.SaveEntry<IMovieData>(entry3);

            // Act
            var results = db.GetAllEntries<IMovieData>();

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, 3);
            Assert.IsTrue(File.Exists(_connectionString));

            //  Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetAllEntries_CollectionName()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);

            var entry1 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 1" };
            var entry2 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 2" };
            var entry3 = new MovieData() { Id = Guid.NewGuid(), DisplayName = "Movie 3" };


            db.SaveEntry<IMovieData>(entry1, "Movies");
            db.SaveEntry<IMovieData>(entry2, "Movies");
            db.SaveEntry<IMovieData>(entry3, "Movies");

            // Act
            var results = db.GetAllEntries<IMovieData>("Movies");

            // Assert
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results.Count, 3);
            Assert.IsTrue(File.Exists(_connectionString));

            //  Clean up
            TearDownTestObjects();
        }

        [TestMethod]
        public void Test_GetAllEntries_NoData()
        {
            // Arrange
            var db = new LiteDBService(_connectionString);
            Exception lastEx = null;
            List<IMovieData> results = null;
            // Act
            try
            {
                results = db.GetAllEntries<IMovieData>();
            }
            catch (Exception ex)
            {
                lastEx = ex;
            }
            // Assert
            Assert.IsNull(lastEx);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 0);
            Assert.IsTrue(File.Exists(_connectionString));

            //  Clean up
            TearDownTestObjects();
        }

        private void TearDownTestObjects()
        {
            if (File.Exists(_connectionString))
                File.Delete(_connectionString);
        }
    }
}
