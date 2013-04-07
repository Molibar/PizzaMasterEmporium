using System;

namespace PizzaMasterEmporium.Framework.Utils
{
    public class StringArrayUtils
    {
        public static string[] Clone(string[] arr)
        {
            if (arr == null) return null;
            var clone = new string[arr.Length];
            Array.Copy(arr, clone, arr.Length);
            return clone;
        }

        public static string[] Insert(string[] arr, string toBeInserted, Location location)
        {
            var withAdded = new string[arr.Length + 1];
            var destinationIndex = location == Location.Append ? 0 : 1;
            var insertIndex = Location.Append == location ? arr.Length : 0;
            Array.Copy(arr, 0, withAdded, destinationIndex, arr.Length);
            withAdded[insertIndex] = toBeInserted;
            return withAdded;
        }

        public static string[] Insert(string[] arr, string[] toBeInserted, Location location)
        {
            var withAdded = new string[arr.Length + toBeInserted.Length];
            var destinationIndex = location == Location.Append ? 0 : toBeInserted.Length;
            var insertIndex = Location.Append == location ? arr.Length : 0;
            Array.Copy(arr, 0, withAdded, destinationIndex, arr.Length);
            Array.Copy(toBeInserted, 0, withAdded, insertIndex, toBeInserted.Length);
            return withAdded;
        }

        public static bool Equals(string[] x, string[] y)
        {
            if (x == null && y == null) return true;
            if (x == null ^ y == null) return false;
            if (ReferenceEquals(x, y)) return true;
            // The hat is an XOR..
            if (x.Length != y.Length) return false;
            for (var i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i]) return false;
            }
            return true;
        }
    }
}