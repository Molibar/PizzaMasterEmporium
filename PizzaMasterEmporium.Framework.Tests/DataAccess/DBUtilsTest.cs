using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.DataAccess;

namespace PizzaMasterEmporium.Framework.Tests.DataAccess
{
    [TestFixture]
    public class DBUtilsTest
    {
        [Test]
        public void ConvertDBGuidNullParameter()
        {
            var result = DBUtils.ConvertDBGuid(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }

        [Test]
        public void ConvertDBGuidEmptyString()
        {
            var result = DBUtils.ConvertDBGuid(String.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }

        [Test]
        public void ConvertDBGuidEmptyGuid()
        {
            var result = DBUtils.ConvertDBGuid(Guid.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }

        [Test]
        public void ConvertDBGuidDBNullValue()
        {
            var result = DBUtils.ConvertDBGuid(DBNull.Value);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }

        [Test]
        public void ConvertDBGuidValidGuid()
        {
            var input = Guid.NewGuid();
            var result = DBUtils.ConvertDBGuid(input);

            Assert.IsNotNull(result);
            Assert.AreEqual(input, result);
        }
    }
}
