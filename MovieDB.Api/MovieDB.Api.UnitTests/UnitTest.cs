using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MovieDB.Api.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        private string _testUrl = "";
        private string _testKey = "";

        [TestMethod]
        public void Test_GetConfig()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            // Act
            var result = api.GetConfiguration().GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_SearchMovies()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = "Tron Legacy";

            // Act
            var results = api.SearchForMovie(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(results.Total_Results > 0, "Expected greater than 0 results.");
            Assert.IsTrue(results.Results.Count > 0, "Expected greater than 0 items in result set.");
            Assert.IsTrue(!String.IsNullOrEmpty(results.Results.First().Original_Title), "Expected non null title");
        }

        [TestMethod]
        public void Test_GetMovieDetail()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = "Tron Legacy";

            var search = api.SearchForMovie(title).GetAwaiter().GetResult();

            var target = search.Results.First();

            // Act
            var detail = api.GetMovie(target.Id).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(detail, "Expected non null result");
            Assert.IsTrue(detail.Genres.Length > 0, "Expected non 0 Genre collection");
        }
    }
}
