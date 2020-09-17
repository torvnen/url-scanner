using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Torvnen.UrlScanner.Tools.DomainExtensionFinder
{
    /// <summary>
    /// To scan a string for URLs, we must know what kind of domain extensions are available.
    /// This class' responsibility is to provide a list of available domain extensions.
    /// </summary>
    public class DomainExtensionFinder
    {
        private readonly HttpClient _client;

        public DomainExtensionFinder()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// Gets a list of available top-level domains from IANA's data site.
        /// </summary>
        /// <returns>
        /// Asynchronous list of domain names. Most likely in all capital letters.
        /// </returns>
        public async IAsyncEnumerable<string> GetTopLevelDomainsAsync()
        {
            var rawTlds = await _client.GetStringAsync("https://data.iana.org/TLD/tlds-alpha-by-domain.txt");

            using var reader = new StringReader(rawTlds);
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // The file might have "comments" that start with #. Skip those.
                if (line.StartsWith("#")) continue;
                yield return line;
            }
        }
    }
}
