using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Torvnen.UrlScanner.UrlExtractor
{
    public class UrlExtractor
    {
        /// <summary>
        /// The pattern for URI extracting.
        /// Allow URIs with protocol or no protocol, allow 
        /// </summary>
        private static readonly string _regexPattern = @"(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
        private readonly Regex _regex = new Regex(_regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Find all URL-like texts from a collection of strings.
        /// Splits the texts first, so a string like "www. example .com" will NOT be a valid url.
        /// However, a string like "www. example.com." Would be a valid URL - it will just drop the www from it.
        /// </summary>
        /// <param name="texts">The strings to "scan"</param>
        /// <returns>IEnumerable{System.Uri} - Using Uri instead of String for sanitary reasons.</returns>
        public IEnumerable<Uri> ExtractUrisFromStrings(params string[] texts)
        {
            foreach (var text in texts)
            {
                var matches = _regex.Matches(text);
                foreach (var match in matches)
                {
                    // It's never null if it matches the RegEx. Coercing a value is safe.
                    var trimmedUri = match.ToString()!
                        // Trim the end because there might be grammatical punctuation in the url.
                        .TrimEnd(',', '.');
                    yield return new Uri(trimmedUri, UriKind.RelativeOrAbsolute);
                }
            }
        }
    }
}
