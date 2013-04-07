using System;

namespace PizzaMasterEmporium.Framework.Helpers
{
    public class Converter
    {
        public static short ToInt16(string s, short defaultValue = 0)
        {
            short value;
            if (!short.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static int ToInt32(string s, int defaultValue = 0)
        {
            int value;
            if (!int.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static long ToInt64(string s, long defaultValue = 0L)
        {
            long value;
            if (!long.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static char ToChar(string s, char defaultValue = (char) 0)
        {
            char value;
            if (!char.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static double ToDouble(string s, double defaultValue = 0d)
        {
            double value;
            if (!double.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static decimal ToDecimal(string s, decimal defaultValue = 0m)
        {
            decimal value;
            if (!decimal.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static bool ToBoolean(string s, bool defaultValue = false)
        {
            bool value;
            if (!bool.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static DateTime ToDateTime(string s)
        {
            DateTime value;
            DateTime.TryParse(s, out value);
            return value;
        }

        /// <summary>
        /// Overloaded since the type can't be compile time constant,
        /// and therefore not possible to have as an optional parameter.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s, DateTime defaultValue)
        {
            DateTime value;
            if (!DateTime.TryParse(s, out value)) value = defaultValue;
            return value;
        }

        public static Guid ToGuid(string s)
        {
            Guid value;
            Guid.TryParse(s, out value);
            return value;
        }


        /// <summary>
        /// Overloaded since the type can't be compile time constant,
        /// and therefore not possible to have as an optional parameter.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(string s, Guid defaultValue)
        {
            Guid value;
            if (!Guid.TryParse(s, out value)) value = defaultValue;
            return value;
        }




        public static int? ToNullableInt32(string value)
        {
            if (value == null) return null;
            return Convert.ToInt32(value);
        }

        public static short? ToNullableInt16(string value)
        {
            if (value == null) return null;
            return Convert.ToInt16(value);
        }

        public static long? ToNullableInt64(string value)
        {
            if (value == null) return null;
            return Convert.ToInt64(value);
        }

        public static decimal? ToNullableDecimal(string value)
        {
            if (value == null) return null;
            return Convert.ToDecimal(value);
        }

        public static Guid? ToNullableGuid(string value)
        {
            if (value == null) return null;
            Guid guid;
            if (Guid.TryParse(value, out guid)) return guid;
            throw new FormatException("Input string was not in a correct format.");
        }

        public static bool? ToNullableBoolean(string value)
        {
            if (value == null) return null;
            bool boolean;
            if (bool.TryParse(value, out boolean)) return boolean;
            throw new FormatException("Input string was not in a correct format.");
        }

        public static T ToEnum<T>(string value)
        {
            if (value == null) return default(T);
            return (T)Enum.Parse(typeof(T), value);
        }

        public static TResult ToEnum<TSource, TResult>(TSource value)
        {
            return (TResult)Enum.Parse(typeof(TResult), value.ToString());
        }
    }
}
