using System.Collections.Generic;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Utils;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    public class StringListUtilsTest
    {

        [Test]
        public void ShouldReturnClonedEmptyListToNewArrayReference()
        {
            // Arrange
            var list = new List<string>();

            // Act
            var result = StringListUtils.Clone(list);

            // Assert
            Assert.That(ReferenceEquals(result, list), Is.False);
            AssertListEquality(result, list);
        }

        [Test]
        public void ShouldReturnCloned_List_With_One_Entry()
        {
            // Arrange
            var list = new List<string> { "One" };

            // Act
            var result = StringListUtils.Clone(list);

            // Assert
            Assert.That(ReferenceEquals(result, list), Is.False);
            AssertListEquality(result, list);
        }

        [Test]
        public void ShouldReturnCloned_List_With_Three_Entries()
        {
            // Arrange
            var list = new[] { "One", "Two", "Three" };

            // Act
            var result = StringListUtils.Clone(list);

            // Assert
            Assert.That(ReferenceEquals(result, list), Is.False);
            AssertListEquality(result, list);
        }


        protected static void AssertListEquality(IList<string> result, IList<string> list)
        {
            Assert.That(result.Count, Is.EqualTo(list.Count));
            for (var i = 0; i < list.Count; i++) Assert.That(result[i], Is.EqualTo(list[i]));
        }
    }
}