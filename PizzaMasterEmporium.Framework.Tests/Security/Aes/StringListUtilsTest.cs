using NUnit.Framework;
using PizzaMasterEmporium.Framework.Security.Aes;

namespace PizzaMasterEmporium.Framework.Tests.Security.Aes
{
    [TestFixture]
    public class SimpleAesTest
    {
        private SimpleAes _simpleAes;

        [SetUp]
        public void SetUp()
        {
            _simpleAes = new SimpleAes();
        }

        [Test]
        public void Should_encrypt_to_another_string_and_be_able_to_decrypt_to_original_string()
        {
            // Arrange
            var text = "!£$%^&*()_+汉字/漢字abcd";

            // Act
            var result = _simpleAes.EncryptString(text);

            // Assert
            Assert.That(result, Is.Not.EqualTo(text));
            Assert.That(_simpleAes.DecryptString(result), Is.EqualTo(text));
        }

        [Test]
        public void Should_encrypt_empty_string_to_empty_string()
        {
            // Arrange
            var text = string.Empty;

            // Act
            var result = _simpleAes.EncryptString(text);

            // Assert
            Assert.That(result, Is.EqualTo(text));
        }

        [Test]
        public void Should_decrypt_empty_string_to_empty_string()
        {
            // Arrange
            var text = string.Empty;

            // Act
            var result = _simpleAes.DecryptString(text);

            // Assert
            Assert.That(result, Is.EqualTo(text));
        }

        [Test]
        public void Should_encrypt_null_to_null()
        {
            // Arrange
            string text = null;

            // Act
            var result = _simpleAes.EncryptString(text);

            // Assert
            Assert.That(result, Is.EqualTo(text));
        }

        [Test]
        public void Should_decrypt_null_to_null()
        {
            // Arrange
            string text = null;

            // Act
            var result = _simpleAes.DecryptString(text);

            // Assert
            Assert.That(result, Is.EqualTo(text));
        }
    }
}