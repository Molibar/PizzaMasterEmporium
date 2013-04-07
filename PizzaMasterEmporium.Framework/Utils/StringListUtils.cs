using System.Collections.Generic;

namespace PizzaMasterEmporium.Framework.Utils
{
    public class StringListUtils
    {
        public static IList<string> Clone(IList<string> strings)
        {
            if (strings == null) return null;
            var array = new string[strings.Count];
            var idx = 0;
            foreach (var str in strings)
            {
                array[idx++] = str;
            }
            return array;
        }

        public static string[] Clone(string[] strings)
        {
            if (strings == null) return null;
            var array = new string[strings.Length];
            var idx = 0;
            foreach (var str in strings)
            {
                array[idx++] = str;
            }
            return array;
        }
    }
}