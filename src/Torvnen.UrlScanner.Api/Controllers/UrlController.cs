using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Torvnen.UrlScanner.Api.Models;

namespace Torvnen.UrlScanner.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly UrlExtractor.UrlExtractor _urlExtractor;

        public UrlController(ILogger<UrlController> logger, UrlExtractor.UrlExtractor urlExtractor)
        {
            _logger = logger;
            _urlExtractor = urlExtractor;
        }

        /// <summary>
        /// Scans the given text input for URL matches.
        /// </summary>
        /// <param name="request">
        /// {
        ///     text: "text to scan for urls"
        /// }
        /// </param>
        /// <returns>
        /// A list of URL-like strings found.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Scan([FromBody]ScanRequest request)
        {
            _logger.LogTrace("Scanning text \"{text}\" for urls", request.Text);

            var urls = _urlExtractor.ExtractUrisFromStrings(request.Text).ToList();

            _logger.LogTrace("Found ${UrlCount} urls", urls.Count);

            return Ok(new ScanResponse(urls));
        }
    }
}
