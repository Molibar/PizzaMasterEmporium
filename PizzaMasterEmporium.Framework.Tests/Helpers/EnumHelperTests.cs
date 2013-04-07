using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Helpers;

namespace PizzaMasterEmporium.Framework.Tests.Helpers
{
    #region Test Enums

    internal enum EnumsNoValues
    {
        TestOne,
        TestTwo,
        TestThree
    }

    internal enum EnumsWithValues
    {
        [System.ComponentModel.Description("Test One")]
        TestOne,

        [System.ComponentModel.Description("Test Two")]
        TestTwo,

        [System.ComponentModel.Description("Test Three")]
        TestThree
    }

    internal enum EnumsWithAlternativeValues
    {
        TestOne = 5,
        TestTwo = 6,
        TestThree = 7
    }

    #endregion

    [TestFixture]
    public class EnumHelperTests
    {
        #region Tests

        #region GetValue

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetValueNullParameter()
        {
            EnumHelper.GetValue(null);
        }

        [Test]
        public void GetValueFromEnumsNoValues()
        {
            var result = EnumHelper.GetValue(EnumsNoValues.TestOne);

            Assert.IsNotNullOrEmpty(result);
            Assert.AreEqual("TestOne", result);
        }

        [Test]
        public void GetValueFromEnumsWithValues()
        {
            var result = EnumHelper.GetValue(EnumsWithValues.TestOne);

            Assert.IsNotNullOrEmpty(result);
            Assert.AreEqual("Test One", result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Value parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetValueNullValue()
        {
            EnumHelper.GetValue(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Value parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetValueEmptyValue()
        {
            EnumHelper.GetValue(String.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Type parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetValueNullEnum()
        {
            EnumHelper.GetValue("TestOne", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "The string is not a description or value of the specified enum.", MatchType = MessageMatch.Exact)]
        public void GetValueInvalidEnumValue()
        {
            EnumHelper.GetValue("TestFour", typeof(EnumsNoValues));
        }

        [Test]
        public void GetValueValidEnumValue()
        {
            var result = EnumHelper.GetValue("TestOne", typeof(EnumsNoValues));

            Assert.IsNotNull(result);
            Assert.AreEqual(EnumsNoValues.TestOne, (EnumsNoValues)result);
        }

        #endregion

        #region GetIntValue

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Value parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetIntValueNullValue()
        {
            EnumHelper.GetIntValue(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Value parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetIntValueEmptyValue()
        {
            EnumHelper.GetIntValue(String.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Enum Type parameter is null or empty", MatchType = MessageMatch.Exact)]
        public void GetIntValueNullEnum()
        {
            EnumHelper.GetIntValue("TestOne", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Unable to return integer value for the specified enum.", MatchType = MessageMatch.Exact)]
        public void GetIntValueInvalidValue()
        {
            EnumHelper.GetIntValue("TestFour", typeof(EnumsNoValues));
        }

        [Test]
        public void GetIntValueValidEnumValue()
        {
            var result = EnumHelper.GetIntValue("TestTwo", typeof(EnumsNoValues));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetIntValueValidEnumValueAlternativeEnums()
        {
            var result = EnumHelper.GetIntValue("TestTwo", typeof(EnumsWithAlternativeValues));

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        #endregion

        #endregion
    }
}
