using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Torvnen.UrlScanner.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Used to check availability of the server.
        /// </summary>
        /// <returns>"pong"</returns>
        [HttpGet]
        public string Ping()
        {
            var callerIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            _logger.LogTrace("Pinged by {callerIpAddress}", callerIpAddress);

            return "pong";
        }
    }
}
