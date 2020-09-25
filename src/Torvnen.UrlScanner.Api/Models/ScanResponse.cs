using System;
using System.Collections.Generic;
using System.Linq;

namespace Torvnen.UrlScanner.Api.Models
{
    public class ScanResponse
    {
        public List<string> Results { get; } = new List<string>();

        public ScanResponse(IEnumerable<Uri> urls)
        {
            if (urls != null)
            {
                Results.AddRange(urls.Select(u => u.ToString()));
            }
        }
    }
}
