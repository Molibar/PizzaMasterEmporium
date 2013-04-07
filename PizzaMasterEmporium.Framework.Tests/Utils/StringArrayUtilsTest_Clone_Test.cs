using NUnit.Framework;
using PizzaMasterEmporium.Framework.Utils;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    [TestFixture]
    public class StringArrayUtilsTest_Clone_Test : StringArrayUtilsTest
    {

        [Test]
        public void ShouldReturnClonedEmptyArrayToNewArrayReference()
        {
            // Arrange
            var arr = new string[0];

            // Act
            var result = StringArrayUtils.Clone(arr);

            // Assert
            Assert.That(ReferenceEquals(result, arr), Is.False);
            AssertArrayEquality(result, arr);
        }

        [Test]
        public void ShouldReturnCloned_Array_With_One_Entry()
        {
            // Arrange
            var arr = new[] {"One"};

            // Act
            var result = StringArrayUtils.Clone(arr);

            // Assert
            Assert.That(ReferenceEquals(result, arr), Is.False);
            AssertArrayEquality(result, arr);
        }

        [Test]
        public void ShouldReturnCloned_Array_With_Three_Entries()
        {
            // Arrange
            var arr = new[] {"One", "Two", "Three"};

            // Act
            var result = StringArrayUtils.Clone(arr);

            // Assert
            Assert.That(ReferenceEquals(result, arr), Is.False);
            AssertArrayEquality(result, arr);
        }

    }
}