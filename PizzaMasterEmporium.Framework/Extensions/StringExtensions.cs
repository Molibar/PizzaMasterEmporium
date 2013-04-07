using System;
using System.Linq;
using System.Text;

namespace PizzaMasterEmporium.Framework.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string EncodeBase64(this string str)
        {
            if (str == null) return null;
            var toEncodeAsBytes = Encoding.Unicode.GetBytes(str);
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeBase64(this string str)
        {
            if (str == null) return null;
            var encodedDataAsBytes = Convert.FromBase64String(str);
            var returnValue = Encoding.Unicode.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static bool In(this string str, params string[] strings)
        {
            return strings.Contains(str);
        }
    }
}