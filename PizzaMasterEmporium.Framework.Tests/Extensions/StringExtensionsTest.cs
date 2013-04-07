using NUnit.Framework;
using PizzaMasterEmporium.Framework.Extensions;

namespace PizzaMasterEmporium.Framework.Tests.Extensions
{
    [TestFixture]
    internal class StringExtensionsTest
    {
        [Test]
        public void ShouldReturn_Not_IsNullOrEmpty_For_String()
        {
            // Arrange
            var expected = false;
            var str = "string";

            // Act
            var result = str.IsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_IsNullOrEmpty_For_Empty()
        {
            // Arrange
            var expected = true;
            var str = string.Empty;

            // Act
            var result = str.IsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_IsNullOrEmpty_For_Null()
        {
            // Arrange
            var expected = true;
            string str = null;

            // Act
            var result = str.IsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Base64_Encoded_String()
        {
            // Arrange
            var expected = "dgBpAHMAaQB0AHMALwAxADIAMwA=";
            var str = "visits/123";

            // Act
            var result = str.EncodeBase64();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_null_if_string_to_be_encoded_is_null()
        {
            // Arrange
            string expected = null;
            string str = null;

            // Act
            var result = str.EncodeBase64();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Base64_Decoded_String()
        {
            // Arrange
            var expected = "visits/123";
            var str = "dgBpAHMAaQB0AHMALwAxADIAMwA=";

            // Act
            var result = str.DecodeBase64();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_null_if_string_to_be_decoded_is_null()
        {
            // Arrange
            string expected = null;
            string str = null;

            // Act
            var result = str.DecodeBase64();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        

        [Test]
        public void Should_return_original_string_if_encoded_and_then_decoded()
        {
            // Arrange
            var complexString = "åäö!#¤%&/(=?воздается國";
            var expected = complexString;

            // Act
            var encoded = complexString.EncodeBase64();
            var result = encoded.DecodeBase64();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}