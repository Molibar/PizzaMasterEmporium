using NUnit.Framework;
using PizzaMasterEmporium.Framework.Extensions;

namespace PizzaMasterEmporium.Framework.Tests.Extensions
{
    [TestFixture]
    public class EnumExtensionTest
    {
        [SetUp]
        public static void SetUp()
        {
            
        }

        [Test]
        public static void ShouldReturnOneForEnumValueOne()
        {
            //Arrange
            string expected = "One";
            var selectedEnumerationEntry = Numbers.One;

            //Act
            var result = selectedEnumerationEntry.GetDisplayString();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public static void ShouldReturnTheStringFromDescriptionAttributeForEnumValueTwo()
        {
            //Arrange
            string expected = "Two InAnotherLanguage";
            var selectedEnumerationEntry = Numbers.Two;

            //Act
            var result = selectedEnumerationEntry.GetDisplayString();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public static void ShouldReturnThreeOrFourSpaceSeparatedForEnumValueThree()
        {
            //Arrange
            string expected = "Three Or Four";
            var selectedEnumerationEntry = Numbers.ThreeOrFour;

            //Act
            var result = selectedEnumerationEntry.GetDisplayString();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public static void ShouldReturnThreeOrFourSpaceSeparatedForEnumValueThree_WhenCallingGetDisplayStringFromValue()
        {
            //Arrange
            string expected = "Three Or Four";
            var selectedEnumerationEntry = Numbers.ThreeOrFour;

            //Act
            var result = selectedEnumerationEntry.GetDisplayStringFromValue();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public static void ShouldReturnTwoForEnumValueTwo_WhenCallingGetDisplayStringFromValue()
        {
            //Arrange
            string expected = "Two";
            var selectedEnumerationEntry = Numbers.Two;

            //Act
            var result = selectedEnumerationEntry.GetDisplayStringFromValue();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        private enum Numbers
        {
            One = 1,
            [System.ComponentModel.Description("Two InAnotherLanguage")]
            Two = 2,
            ThreeOrFour = 3
        }
    }

}
