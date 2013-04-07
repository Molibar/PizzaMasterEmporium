using System;
using System.Text.RegularExpressions;

namespace PizzaMasterEmporium.Framework.Helpers
{
    public class TextHelper
    {
        public static Object StripNonNumeric(String input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null");

            String result = Regex.Replace(input, "[^.0-9]", "");

            if (!String.IsNullOrEmpty(result))
                return result;
            
            return null;
        }

        public static String Left(String input, int length)
        {
            if (input == null)
                throw new ArgumentException("Input parameter cannot be null");

            if (length < 0)
                throw new ArgumentException("Length parameter must be greater than zero");

            if (input.Length == 0 || length == 0)
                return String.Empty;

            if (input.Length <= length)
                return input;

            return input.Substring(0, length);
        }
    }
}
