using NUnit.Framework;
using PizzaMasterEmporium.Framework.Utils;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    [TestFixture]
    public class StringArrayUtils_Insert_Test : StringArrayUtilsTest
    {

        [Test]
        public void ShouldReturnClonedArray_With_New_Prepended()
        {
            // Arrange
            var location = Location.Prepend;
            var arr = new[] { "One", "Two", "Three" };
            var toBeInserted = "New";

            // Act
            var result = StringArrayUtils.Insert(arr, toBeInserted, location);

            // Assert
            Assert.That(result.Length, Is.EqualTo(arr.Length + 1));
            Assert.That(result[0], Is.EqualTo(toBeInserted));
            Assert.That(result[1], Is.EqualTo(arr[0]));
            Assert.That(result[2], Is.EqualTo(arr[1]));
            Assert.That(result[3], Is.EqualTo(arr[2]));
        }

        [Test]
        public void ShouldReturnClonedArray_With_New_Appended()
        {
            // Arrange
            var location = Location.Append;
            var arr = new[] { "One", "Two", "Three" };
            var toBeInserted = "New";

            // Act
            var result = StringArrayUtils.Insert(arr, toBeInserted, location);

            // Assert
            Assert.That(result.Length, Is.EqualTo(arr.Length + 1));
            Assert.That(result[result.Length - 1], Is.EqualTo(toBeInserted));
            Assert.That(result[0], Is.EqualTo(arr[0]));
            Assert.That(result[1], Is.EqualTo(arr[1]));
            Assert.That(result[2], Is.EqualTo(arr[2]));
            Assert.That(result[3], Is.EqualTo(toBeInserted));
        }

        [Test]
        public void ShouldReturnClonedArray_With_New_Array_Prepended()
        {
            // Arrange
            var location = Location.Prepend;
            var arr = new[] { "Three", "Four", "Five" };
            var toBeInserted = new[] { "One", "Two" };

            // Act
            var result = StringArrayUtils.Insert(arr, toBeInserted, location);

            // Assert
            Assert.That(result.Length, Is.EqualTo(arr.Length + toBeInserted.Length));
            Assert.That(result[0], Is.EqualTo(toBeInserted[0]));
            Assert.That(result[1], Is.EqualTo(toBeInserted[1]));
            Assert.That(result[2], Is.EqualTo(arr[0]));
            Assert.That(result[3], Is.EqualTo(arr[1]));
            Assert.That(result[4], Is.EqualTo(arr[2]));
        }

        [Test]
        public void ShouldReturnClonedArray_With_New_Array_Appended()
        {
            // Arrange
            var location = Location.Append;
            var arr = new[] { "One", "Two", "Three" };
            var toBeInserted = new[] { "Four", "Five" };

            // Act
            var result = StringArrayUtils.Insert(arr, toBeInserted, location);

            // Assert
            Assert.That(result.Length, Is.EqualTo(arr.Length + toBeInserted.Length));
            Assert.That(result[0], Is.EqualTo(arr[0]));
            Assert.That(result[1], Is.EqualTo(arr[1]));
            Assert.That(result[2], Is.EqualTo(arr[2]));
            Assert.That(result[3], Is.EqualTo(toBeInserted[0]));
            Assert.That(result[4], Is.EqualTo(toBeInserted[1]));
        }
    }
}