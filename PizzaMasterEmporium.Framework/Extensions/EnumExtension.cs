using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PizzaMasterEmporium.Framework.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayString(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            var attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? enumValue.GetDisplayStringFromValue() : attribute.Description;
        }

        internal static string GetDisplayStringFromValue(this Enum enumValue)
        {
            var result = Regex.Replace(enumValue.ToString(), @"(?<a>(?<!^)((?:[A-Z][a-z])|(?:(?<!^[A-Z]+)[A-Z0-9]+(?:(?=[A-Z][a-z])|$))|(?:[0-9]+)))", @" ${a}");

            return result;
        }
    }
}
