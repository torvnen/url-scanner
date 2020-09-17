using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Torvnen.UrlScanner.Api;

namespace Torvnen.UrlScanner.Tests.Integration
{
    /// <summary>
    /// Health-check test for the API.
    /// </summary>
    public class PingTest
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PingTest()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Test]
        public async Task Get_Ping_ShouldReturnPong()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetStringAsync("/ping");

            // Assert
            Assert.NotNull(response);
            Assert.True(response.ToLower().Contains("pong"));
        }
    }
}