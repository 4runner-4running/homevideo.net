using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MovieDB.Api.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        private string _testUrl = "";
        private string _testKey = "01440bbed273601848b2bcebe48cc465";
        private string _test_output_path = @"..\..\..\TestOutput";

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
            Assert.AreEqual("TRON: Legacy", results.Results.First().Original_Title);
            Assert.AreEqual(20526, results.Results.First().Id);
        }

        [TestMethod]
        public void Test_GetMovieDetail()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = 20526;

            // Act
            var detail = api.GetMovie(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(detail, "Expected non null result");
            Assert.IsTrue(detail.Genres.Length > 0, "Expected non 0 Genre collection");
        }

        [TestMethod]
        public void Test_GetMovieImage()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = "/vuifSABRpSnxCAOxEnWpNbZSXpp.jpg";

            // Act
            Stream stream = api.GetMovieImage(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(stream.Length > 0);

            //Cleanup
            stream.Dispose();
        }

        [TestMethod]
        public void Test_GetMovieImageIsValidImage()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = "/vuifSABRpSnxCAOxEnWpNbZSXpp.jpg";
            var imageSaved = false;
            Exception ex = null;
            Image img = null;

            // Act
            var stream = api.GetMovieImage(title).GetAwaiter().GetResult();

            // Try to make Image and save
            try
            {
                img = Image.FromStream(stream);
                stream.Position = 0;
                img.Save($@"..\..\..\TestOutput{title}");
                imageSaved = true;
            }
            catch (Exception x)
            {
                ex = x;
                img?.Dispose();
                stream?.Dispose();
            }

            // Assert
            Assert.IsTrue(imageSaved);
            Assert.IsNull(ex);
            Assert.IsNotNull(img);
            Assert.IsTrue(img.Width > 0);
            Assert.IsTrue(img.Height > 0);
            Assert.IsTrue(File.Exists($"{_test_output_path}{title}"));

            // Cleanup
            img?.Dispose();
            stream?.Dispose();
            if (File.Exists($"{_test_output_path}{title}"))
                File.Delete($"{_test_output_path}{title}");
        }

        [TestMethod]
        public void Test_GetMovieImageAsThumbnail()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var imagePath = "/vuifSABRpSnxCAOxEnWpNbZSXpp.jpg";
            Image img = null;
            Exception ex = null;

            // Act
            var stream = api.GetMovieImage(imagePath, true).GetAwaiter().GetResult();
            try
            {
                img = Image.FromStream(stream);
            }
            catch (Exception x) 
            {
                ex = x;
                if (img != null)
                    img.Dispose();

                if (stream != null)
                    stream.Dispose();
            }

            // Assert
            Assert.IsTrue(img.Width == 185);
            Assert.IsNull(ex);

            // Cleanup
            img?.Dispose();
            stream?.Dispose();
        }

        [TestMethod]
        public void Test_SearchTv()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var title = "Avatar the last Airbender";

            // Act
            var results = api.SearchForTv(title).GetAwaiter().GetResult();

            // Assert
            Assert.IsTrue(results.Total_Results > 0, "Expected greater than 0 results.");
            Assert.IsTrue(results.Results.Count > 0, "Expected greater than 0 items in result set.");
            Assert.IsTrue(!String.IsNullOrEmpty(results.Results.First().Original_Name));
            Assert.AreEqual("Avatar: The Last Airbender", results.Results.First().Original_Name);
            Assert.AreEqual(246, results.Results.First().Id);
        }

        [TestMethod]
        public void Test_GetTvDetail()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var id = 246;

            // Act
            var detail = api.GetTvShow(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(detail, "Expected non null result");
            Assert.AreEqual("Avatar: The Last Airbender", detail.Name);
        }

        [TestMethod]
        public void Test_GetSeason()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var showId = 246;
            var season = 1;

            // Act
            var detail = api.GetSeason(showId, season).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(detail, "Expected non null result");
            Assert.AreEqual("Book One: Water", detail.Name);
            Assert.AreEqual(20, detail.Episodes.Length);
        }

        [TestMethod]
        public void Test_GetEpisode()
        {
            // Arrange
            var api = new MovieDBApi(_testKey);
            var showId = 246;
            var seasonNumber = 1;
            var episodeNumber = 1;

            // Act
            var detail = api.GetEpisode(showId, seasonNumber, episodeNumber).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(detail, "Expected non null result");
            Assert.AreEqual("Chapter One: The Boy in the Iceberg", detail.Name);
            Assert.AreEqual(seasonNumber, detail.Season_Number);
            Assert.AreEqual(episodeNumber, detail.Episode_Number);
        }
    }
}
