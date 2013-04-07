
using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Helpers;

namespace PizzaMasterEmporium.Framework.Tests.Helpers
{
    public class UriHelper_Combine_Test : UriHelper
    {

        [Test]
        public void ShouldBeAbleToCombineTwoHalvesOfAUrl_FirstPartEndingWithSlash_SecondPartBeginningWithSlash()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Banana-Bandana-Ninja.aspx";
            var firstPart = "http://www.contoso.com/Active/";
            var lastPart = "/Playing/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, lastPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldBeAbleToCombineTwoHalvesOfAUrl_FirstPartNotEndingWithSlash_SecondPartBeginningWithSlash()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Banana-Bandana-Ninja.aspx";
            var firstPart = "http://www.contoso.com/Active";
            var lastPart = "/Playing/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, lastPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldBeAbleToCombineTwoHalvesOfAUrl_FirstPartEndingWithSlash_SecondPartNotBeginningWithSlash()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Banana-Bandana-Ninja.aspx";
            var firstPart = "http://www.contoso.com/Active/";
            var lastPart = "Playing/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, lastPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldBeAbleToCombineTwoHalvesOfAUrl_FirstPartNotEndingWithSlash_SecondPartNotBeginningWithSlash()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Banana-Bandana-Ninja.aspx";
            var firstPart = "http://www.contoso.com/Active";
            var lastPart = "Playing/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, lastPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldBeAbleToCombineMultiplePartsIntoOneLongUrl()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Random/Banana-Bandana-Ninja.aspx";
            var firstPart = "http://www.contoso.com/Active";
            var secondPart = "Playing";
            var thirdPart = "Random/";
            var fourthPart = "/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, secondPart, thirdPart, fourthPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ShouldThrowExceptionIf_firstPart_null()
        {
            // Arrange
            var expected = "http://www.contoso.com/Active/Playing/Banana-Bandana-Ninja.aspx";
            string firstPart = null;
            var lastPart = "Playing/Banana-Bandana-Ninja.aspx";

            // Act
            var uri = UriHelper.Combine(firstPart, lastPart);

            // Assert
            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected));
        }
    }
}