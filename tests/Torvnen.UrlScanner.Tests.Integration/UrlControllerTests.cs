using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using Torvnen.UrlScanner.Api;
using Torvnen.UrlScanner.Api.Models;

namespace Torvnen.UrlScanner.Tests.Integration
{
    /// <summary>
    /// End-to-end tests for the Scanning API
    /// </summary>
    public class UrlControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UrlControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        private async Task<List<string>> ExtractResultsFromHttpResponse(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ScanResponse>(responseContent).Results;
        }

        [Test]
        public async Task POST_Scan_ShouldReturnSuccess()
        {
            var client = _factory.CreateClient();

            // Use anonymous object in order to simulate a non-DotNet client
            var dataObject = new
            {
                text = "This text does not matter, as this test only checks for a success code."
            };
            // Serialize content
            var dataString = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(dataObject));
            // Create the request body. Remember Content-Type header!
            var httpContent = new StringContent(dataString, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/url/scan", httpContent);

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            // Just for the sake of security, make sure that there are no actual URLs found in the text :)
            var urls = await ExtractResultsFromHttpResponse(response);
            Assert.IsEmpty(urls);
        }

        [Test]
        public async Task POST_Scan_ShouldReturnMultipleUrlResults_ForAllDataSets()
        {
            var client = _factory.CreateClient();
            var testDataFilePaths = Directory.EnumerateFiles("./input-strings");

            foreach (var testDataFilePath in testDataFilePaths)
            {
                var testData = await File.ReadAllTextAsync(testDataFilePath);
                // Use anonymous object in order to simulate a non-DotNet client
                var dataObject = new
                {
                    text = testData
                };
                // Serialize content
                var dataString = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(dataObject));
                // Create the request body. Remember Content-Type header!
                var httpContent = new StringContent(dataString, Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync("/url/scan", httpContent);
                var urls = await ExtractResultsFromHttpResponse(response);

                // Assert
                Assert.IsNotEmpty(urls, "TestData should have URLs in it. TestData: {0}", testData);

                // Test data set 4 has 6 URLs in it - check for that
                if (testDataFilePath.EndsWith("4.txt"))
                {
                    Assert.AreEqual(6, urls.Count());
                }
            }
        }
    }
}
