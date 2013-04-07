using System;
using System.Globalization;

namespace PizzaMasterEmporium.Framework.Utils
{
    public class StringUtils
    {

        /// <summary>
        /// If "application/page.aspx?user=bill" is sent in and c = '?' then "application/page.aspx" will be returned.
        /// </summary>
        /// <param name="c">the char to begin stripping from</param>
        /// <param name="str">the string to strip away the ending from</param>
        /// <returns>The stripped string</returns>
        public static string StripFromChar(char c, string str)
        {
            if (str == null) throw new ArgumentException("The argument str is null");
            int idx = str.IndexOf(c);
            if (idx < 0) return str;
            return str.Substring(0, idx);
        }


        public static string Substring(string fullString, int maxLength)
        {
            return fullString.Length > maxLength ? fullString.Remove(maxLength) : fullString;
        }


        public static string PadValue(int value, int requiredLength,
            char character = '0', Location location = Location.Prepend)
        {
            var valueAsString = value.ToString(CultureInfo.InvariantCulture);
            var requiredPadding = requiredLength - valueAsString.Length;
            var padding = new string(character, requiredPadding);
            return (location == Location.Prepend)
                ? string.Concat(padding, valueAsString)
                : string.Concat(valueAsString, padding);
        }
    }
}
