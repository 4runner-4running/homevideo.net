using HomeVideo.Net.Indexing;
using HomeVideo.Net.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeVideo.Net.UnitTests
{
    [TestClass]
    public class IndexingTests
    {
        [TestMethod]
        public void MovieIndexer_GetFiles()
        {
            // Arrange
            var logger = new LiteDBLogger();
            var indexer = new MovieIndexer(logger, "test-movie-Index", @"L:\Movies");

            // Act
            var result = indexer.Index().GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result.ResultCount > 0);
        }
    }
}
