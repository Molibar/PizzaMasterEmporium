using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Helpers;

namespace PizzaMasterEmporium.Framework.Tests.Common
{
    [TestFixture]
    public class ConverterTest
    {

        [Test]
        public void ShouldReturn_Default_Decimal_ForString_null_ToDecimal()
        {
            // Arrange
            var expected = 0m;
            string input = null;

            // Act
            var result = Converter.ToDecimal(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_sent_in_Default_Decimal_ForString_null_ToDecimal()
        {
            // Arrange
            var expected = 1.23m;
            string input = null;

            // Act
            var result = Converter.ToDecimal(input, expected);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Default_Guid_ForString_null_ToGuid()
        {
            // Arrange
            var expected = new Guid();
            string input = null;

            // Act
            var result = Converter.ToGuid(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_sent_in_Default_Guid_ForString_null_ToGuid()
        {
            // Arrange
            var expected = Guid.NewGuid();
            string input = null;

            // Act
            var result = Converter.ToGuid(input, expected);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Default_DateTime_ForString_null_ToDateTime()
        {
            // Arrange
            Guid expected = new Guid();
            string input = null;

            // Act
            var result = Converter.ToGuid(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_sent_in_Default_DateTime_ForString_null_ToDateTime()
        {
            // Arrange
            var expected = DateTime.Now;
            string input = null;

            // Act
            var result = Converter.ToDateTime(input, expected);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void ShouldReturn_null_ForString_null_ToInt32()
        {
            // Arrange
            int? expected = null;
            string input = null;

            // Act
            var result = Converter.ToNullableInt32(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_3_ForString_3_ToInt32()
        {
            // Arrange
            var expected = 3;
            var input = "3";

            // Act
            var result = Converter.ToNullableInt32(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrow_FormatException_ForString_keff_ToInt32()
        {
            // Arrange
            var input = "keff";

            // Act
            var result = Converter.ToNullableInt32(input);

            // Assert
        }

        [Test]
        public void ShouldReturn_null_ForString_null_ToInt16()
        {
            // Arrange
            int? expected = null;
            string input = null;

            // Act
            var result = Converter.ToNullableInt16(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_3_ForString_3_ToInt16()
        {
            // Arrange
            var expected = 3;
            var input = "3";

            // Act
            var result = Converter.ToNullableInt16(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrow_FormatException_ForString_keff_ToInt16()
        {
            // Arrange
            var input = "keff";

            // Act
            var result = Converter.ToNullableInt16(input);

            // Assert
        }

        [Test]
        public void ShouldReturn_null_ForString_null_ToInt64()
        {
            // Arrange
            int? expected = null;
            string input = null;

            // Act
            var result = Converter.ToNullableInt64(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_3_ForString_3_ToInt64()
        {
            // Arrange
            var expected = 3;
            var input = "3";

            // Act
            var result = Converter.ToNullableInt64(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrow_FormatException_ForString_keff_ToInt64()
        {
            // Arrange
            var input = "keff";

            // Act
            var result = Converter.ToNullableDecimal(input);

            // Assert
        }


        [Test]
        public void ShouldReturn_null_ForString_null_ToDecimal()
        {
            // Arrange
            int? expected = null;
            string input = null;

            // Act
            var result = Converter.ToNullableDecimal(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_3_14_ForString_3_14_ToDecimal()
        {
            // Arrange
            var expected = 3.14m;
            var input = "3.14";

            // Act
            var result = Converter.ToNullableDecimal(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrow_FormatException_ForString_keff_ToDecimal()
        {
            // Arrange
            var input = "keff";

            // Act
            var result = Converter.ToNullableInt64(input);

            // Assert
        }


        [Test]
        public void ShouldReturn_null_ForString_null_ToGuid()
        {
            // Arrange
            Guid? expected = null;
            string input = null;

            // Act
            var result = Converter.ToNullableGuid(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void ShouldReturn_SameGuid_ForString_Guid_ToGuid()
        {
            // Arrange
            Guid? expected = new Guid("017d090d-55d7-4261-aebe-eb8a926381c3");
            var input = "017d090d-55d7-4261-aebe-eb8a926381c3";

            // Act
            var result = Converter.ToNullableGuid(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ShouldThrow_FormatException_ForString_keff_ToGuid()
        {
            // Arrange
            var input = "keff";

            // Act
            var result = Converter.ToNullableGuid(input);

            // Assert
        }


        [Test]
        public void ShouldReturn_Default_IfInput_Null()
        {
            // Arrange
            TestEnum expected = 0;
            string input = null;

            // Act
            var result = Converter.ToEnum<TestEnum>(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Zero_IfInput_Zero()
        {
            // Arrange
            TestEnum expected = TestEnum.Zero;
            string input = "Zero";

            // Act
            var result = Converter.ToEnum<TestEnum>(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_Two_IfInput_Two()
        {
            // Arrange
            TestEnum expected = TestEnum.Two;
            string input = "Two";

            // Act
            var result = Converter.ToEnum<TestEnum>(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_TestEnumZero_IfInput_OtherTestEnumZero()
        {
            // Arrange
            TestEnum expected = TestEnum.Zero;

            // Act
            var result = Converter.ToEnum<OtherTestEnum, TestEnum>(OtherTestEnum.Zero);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturn_TestEnumTwo_IfInput_OtherTestEnumTwo()
        {
            // Arrange
            TestEnum expected = TestEnum.Two;

            // Act
            var result = Converter.ToEnum<OtherTestEnum, TestEnum>(OtherTestEnum.Two);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        public enum TestEnum
        {
            Zero = 0,
            One = 1,
            Two = 2
        }

        public enum OtherTestEnum
        {
            Zero = 2,
            One = 1,
            Two = 0
        }
    }
}
