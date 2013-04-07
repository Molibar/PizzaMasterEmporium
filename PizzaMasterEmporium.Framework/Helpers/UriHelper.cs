using System;
using System.Text;

namespace PizzaMasterEmporium.Framework.Helpers
{
    public class UriHelper
    {
        public static Uri Combine(string firstPart, params string[] fragments)
        {
            if (firstPart == null)
            {
                throw new ArgumentNullException("firstPart");
            }
            var sb = new StringBuilder(firstPart);
            foreach (var fragment in fragments)
            {
                if (string.IsNullOrEmpty(fragment)) return null;
                var firstPartEndsWithSlash = sb[sb.Length - 1] == '/';
                var lastPartStartsWithSlash = fragment.StartsWith("/");
                if (firstPartEndsWithSlash)
                {
                    // if firstPart ends with '/' and lastPart starts with '/' then
                    // one should skip the first '/' from the lastPart.
                    sb.Append(lastPartStartsWithSlash ? fragment.Substring(1) : fragment);
                }
                else
                {
                    // if firstPart doesn't end with '/' and lastPart doesn't start
                    // with '/' one should append a '/' before appending lastPart.
                    if (lastPartStartsWithSlash) sb.Append(fragment);
                    else sb.Append("/").Append(fragment);
                }
            }
            var uri = new Uri(sb.ToString());
            return uri;
        }
    }
}