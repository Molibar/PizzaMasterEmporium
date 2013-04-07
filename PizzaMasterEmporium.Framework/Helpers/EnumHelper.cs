using System;
using System.ComponentModel;
using System.Reflection;

namespace PizzaMasterEmporium.Framework.Helpers
{
    public static class EnumHelper
    {
        public static string GetValue(Enum value)
        {
            if (value == null)
                throw new ArgumentException("Enum parameter is null or empty");

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            
            return value.ToString();
        }

        public static object GetValue(string value, Type enumType)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("Enum Value parameter is null or empty");

            if (enumType == null)
                throw new ArgumentException("Enum Type parameter is null or empty");

            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (GetValue((Enum)Enum.Parse(enumType, name)).Equals(value))
                    return Enum.Parse(enumType, name);
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

        public static int GetIntValue(String value, Type enumType)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("Enum Value parameter is null or empty");

            if (enumType == null)
                throw new ArgumentException("Enum Type parameter is null or empty");

            String[] names = Enum.GetNames(enumType);
            foreach (String name in names)
            {
                if (GetValue((Enum)Enum.Parse(enumType, name)).Equals(value))
                    return (int)Enum.Parse(enumType, name);
            }

            throw new ArgumentException("Unable to return integer value for the specified enum.");
        }
    }
}