using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Torvnen.UrlScanner.Tools.DomainExtensionFinder;

namespace Torvnen.UrlScanner.Tests.Unit
{
    /// <summary>
    /// <inheritdoc cref="DomainExtensionFinder"/>
    /// </summary>
    public class DomainExtensionFinderTests
    {
        private DomainExtensionFinder _finder;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _finder = new DomainExtensionFinder();
        }

        [Test]
        public async Task Finder_GetTlds_ShouldNotBeEmpty()
        {
            // Act
            var tlds = await _finder.GetTopLevelDomainsAsync().ToListAsync();

            // Assert
            Assert.IsNotEmpty(tlds);
        }

        [Test]
        public async Task Finder_GetTlds_ShouldNotContainLeadingHashmarks()
        {
            // Act
            var tlds = await _finder.GetTopLevelDomainsAsync().ToListAsync();

            // Assert
            Assert.IsTrue(tlds.TrueForAll(tld => !tld.StartsWith("#")));
        }
    }
}