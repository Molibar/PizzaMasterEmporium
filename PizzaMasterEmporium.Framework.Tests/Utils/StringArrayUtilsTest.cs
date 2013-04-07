using NUnit.Framework;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    [TestFixture]
    public class StringArrayUtilsTest
    {
        protected static void AssertArrayEquality(string[] result, string[] arr)
        {
            Assert.That(result.Length, Is.EqualTo(arr.Length));
            for (var i = 0; i < arr.Length; i++) Assert.That(result[i], Is.EqualTo(arr[i]));
        }
    }
}
