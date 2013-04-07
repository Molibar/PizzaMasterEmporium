using NUnit.Framework;
using PizzaMasterEmporium.Framework.Utils;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    public class StringArrayUtils_Equals_Test : StringArrayUtilsTest
    {
        [Test]
        public void ShouldReturn_True_For_Array_And_Self()
        {
            // Arrange
            var arrOne = new[] { "One", "Two", "Three" };

            // Act
            var result = StringArrayUtils.Equals(arrOne, arrOne);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturn_True_For_Array_And_Clone()
        {
            // Arrange
            var arrOne = new[] { "One", "Two", "Three" };
            var arrTwo = StringArrayUtils.Clone(arrOne);

            // Act
            var result = StringArrayUtils.Equals(arrOne, arrTwo);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturn_True_For_Two_Identical_Arrays_With_One_Items()
        {
            // Arrange
            var arrOne = new[] { "One" };
            var arrTwo = new[] { "One" };

            // Act
            var result = StringArrayUtils.Equals(arrOne, arrTwo);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturn_True_For_Two_Identical_Arrays_With_Three_Items()
        {
            // Arrange
            var arrOne = new[] { "One", "Two", "Three" };
            var arrTwo = new[] { "One", "Two", "Three" };

            // Act
            var result = StringArrayUtils.Equals(arrOne, arrTwo);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturn_True_For_Two_Null_Arrays()
        {
            // Arrange
            string[] arrOne = null;
            string[] arrTwo = null;

            // Act
            var result = StringArrayUtils.Equals(arrOne, arrTwo);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturn_False_If_Only_One_Array_Is_Null()
        {
            // Arrange
            var arrOne = new string[0];

            // Act
            var result = StringArrayUtils.Equals(arrOne, null) || StringArrayUtils.Equals(null, arrOne); 

            // Assert
            Assert.That(result, Is.False);
        }
    }
}