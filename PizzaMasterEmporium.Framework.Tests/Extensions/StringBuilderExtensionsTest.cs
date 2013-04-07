using System.Text;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Extensions;

namespace PizzaMasterEmporium.Framework.Tests.Extensions
{
    [TestFixture]
    public class StringBuilderExtensionsTest
    {
        private const string SEPARATOR = "|";

        [Test]
        public void Should_NotAddSeparator_If_Only_oneString()
        {
            // Arrange
            var expected = "Hello";
            var stringBuilder = new StringBuilder();
            var oneString = "Hello";

            // Act
            var result = stringBuilder.AppendSeparatedString(oneString, SEPARATOR).ToString();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_AddSeparator_If_oneString_And_anotherString()
        {
            // Arrange
            var expected = "Hello|Ali";
            var stringBuilder = new StringBuilder();
            var oneString = "Hello";
            var anotherString = "Ali";

            // Act
            var result = stringBuilder.AppendSeparatedString(oneString, SEPARATOR)
                .AppendSeparatedString(anotherString, SEPARATOR).ToString();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}