using HomeVideo.Net.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.UnitTests
{
    [TestClass]
    public class MetadataServiceTests
    { 
        private readonly string _apiKey = "";
        
        [TestMethod]
        public void Test_SearchMatching()
        {
            // Arrange
            var mdService = new MetadataService(_apiKey);
            var title = "John Wick";

            // Act
            var result = mdService.GetMovieByTitle(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result.DisplayName.ToLower() == title.ToLower());
            Assert.IsFalse(result.DisplayName.ToLower() == "john wick: chapter 3 - parabellum"); // Original return has chapter 3 as the first result
        }

        [TestMethod]
        public void Test_SearchMatching2()
        {
            // Arrange
            var mdService = new MetadataService(_apiKey);
            var title = "John Wick: Chapter 3 - Parabellum";

            // Act
            var result = mdService.GetMovieByTitle(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(result.DisplayName.ToLower() == title.ToLower());
            Assert.IsFalse(result.DisplayName.ToLower() == "john wick"); // Original return has chapter 3 as the first result
        }

        [TestMethod]
        public void Test_SearchMatching3()
        {
            // Arrange
            var mdService = new MetadataService(_apiKey);
            var title = "Dumb & Dumber";

            // Act
            var result = mdService.GetMovieByTitle(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue((result.DisplayName.ToLower() == title.ToLower() || result.DisplayName.ToLower() == "dumb and dumber"));
            Assert.IsFalse(result.DisplayName.ToLower() == "dumb and dumberer"); // Original return has chapter 3 as the first result
        }
    }
}
