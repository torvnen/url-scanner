using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Torvnen.UrlScanner.Tests.Unit
{
    public class UrlExtractorTests
    {
        private readonly UrlExtractor.UrlExtractor _sp;

        public UrlExtractorTests()
        {
            _sp = new UrlExtractor.UrlExtractor();
        }

        [Test]
        public void Extractor_ShouldReturnSomeUrls_ForAllTestData()
        {
            // Arrange
            var testDataFilePaths = Directory.EnumerateFiles("./input-strings");

            foreach (var testDataFilePath in testDataFilePaths)
            {
                var testData = File.ReadAllText(testDataFilePath);

                // Act
                var urls = _sp.ExtractUrisFromStrings(testData).ToList();

                // Assert
                Assert.IsNotEmpty(urls, "TestData should have URLs in it. TestData: {0}", testData);

                // Test data set 4 has 6 URLs in it - check for that
                if (testDataFilePath.EndsWith("4.txt"))
                {
                    Assert.AreEqual(6, urls.Count);
                }
            }
        }
    }
}
