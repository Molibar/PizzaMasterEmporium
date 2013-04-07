using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Helpers;

namespace PizzaMasterEmporium.Framework.Tests.Helpers
{
    [TestFixture]
    public class TextHelperTest
    {
        #region StripNonNumeric Tests

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Input cannot be null", MatchType = MessageMatch.Exact)]
        public void StripNonNumericNullParameter()
        {
            TextHelper.StripNonNumeric(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Input cannot be null", MatchType = MessageMatch.Exact)]
        public void StripNonNumericEmptyString()
        {
            TextHelper.StripNonNumeric(String.Empty);
        }

        [Test]
        public void StripNonNumericAllNumbers()
        {
            String input = "123";
            var result = TextHelper.StripNonNumeric(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result);
        }

        [Test]
        public void StripNonNumericAllText()
        {
            String input = "ABC";
            var result = TextHelper.StripNonNumeric(input);

            Assert.IsNull(result);
        }

        [Test]
        public void StripNonNumericCombination()
        {
            String input = "ABC123";
            var result = TextHelper.StripNonNumeric(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result);
        }

        [Test]
        public void StripNonNumericSpecialCharacters()
        {
            String input = "123!£$%&@?";
            var result = TextHelper.StripNonNumeric(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result);
        }

        [Test]
        public void StripNonNumericDecimal()
        {
            String input = "12.3";
            var result = TextHelper.StripNonNumeric(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("12.3", result);
        }

        #endregion

        #region Left Tests

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Input parameter cannot be null", MatchType = MessageMatch.Exact)]
        public void LeftNullParameters()
        {
            TextHelper.Left(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Length parameter must be greater than zero", MatchType = MessageMatch.Exact)]
        public void LeftBlankStringNegativeLength()
        {
            TextHelper.Left(String.Empty, -5);
        }
        
        [Test]
        public void LeftBlankStringZeroLength()
        {
            var result = TextHelper.Left(String.Empty, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(String.Empty, result);
        }

        [Test]
        public void LeftBlankStringWithLength()
        {
            var result = TextHelper.Left(String.Empty, 5);

            Assert.IsNotNull(result);
            Assert.AreEqual(String.Empty, result);
        }

        [Test]
        public void LeftWithStringZeroLength()
        {
            var result = TextHelper.Left("ABC", 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(String.Empty, result);
        }

        [Test]
        public void LeftSameLengthAsString()
        {
            var result = TextHelper.Left("ABC", 3);

            Assert.IsNotNull(result);
            Assert.AreEqual("ABC", result);
        }

        [Test]
        public void LeftLongStringWithLength()
        {
            var result = TextHelper.Left("ABCDEF", 3);

            Assert.IsNotNull(result);
            Assert.AreEqual("ABC", result);
        }

        #endregion
    }
}
