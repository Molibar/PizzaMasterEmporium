using System.Text;

namespace PizzaMasterEmporium.Framework.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSeparatedString(this StringBuilder stringBuilder, string value, string separator)
        {
            if (stringBuilder.Length > 0) stringBuilder.Append(separator);
            stringBuilder.Append(value);
            return stringBuilder;
        }
    }
}