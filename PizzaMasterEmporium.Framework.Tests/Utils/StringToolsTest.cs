using NUnit.Framework;
using PizzaMasterEmporium.Framework.Utils;

namespace PizzaMasterEmporium.Framework.Tests.Utils
{
    [TestFixture]
    public class StringToolsTest
    {
        [Test]
        public void Should_return_application_page_aspx_for_application_page_aspx_user_bill_when_char_is_questionmark()
        {
            // Arrange
            var expected = "application/page.aspx";
            var character = '?';
            var filename = "application/page.aspx?user=bill";

            // Act
            var result = StringUtils.StripFromChar(character, filename);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_Return_0000_For_PadValue_if_Value0_and_requiredLength4()
        {
            // Arrange
            var expected = "0000";
            var value = 0;

            // Act
            var result = StringUtils.PadValue(value, 4);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void Should_Return_0001_For_PadValue_if_Value1_and_requiredLength4()
        {
            // Arrange
            var expected = "0001";
            var value = 1;

            // Act
            var result = StringUtils.PadValue(value, 4);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void Should_Return_1200_For_PadValue_if_Value0_requiredLength4_and_LocationAppend()
        {
            // Arrange
            var expected = "1200";
            var value = 12;

            // Act
            var result = StringUtils.PadValue(value, 4, location: Location.Append);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void Should_Return_xxx0_For_PadValue_if_Value0_and_requiredLength4()
        {
            // Arrange
            var expected = "xxx0";
            var value = 0;

            // Act
            var result = StringUtils.PadValue(value, 4, 'x');

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
