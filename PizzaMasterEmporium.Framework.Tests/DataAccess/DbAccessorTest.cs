using NUnit.Framework;
using PizzaMasterEmporium.Framework.DataAccess;

namespace PizzaMasterEmporium.Framework.Tests.DataAccess
{
    [TestFixture]
    public class DbAccessorTest
    {
        private DbAccessor _dbAccessor;

        [SetUp]
        public void SetUp()
        {
            _dbAccessor =
                new DbAccessor(
                    new DatabaseConnectionStringProvider(
                        "Data Source=SIDESWIPE;Database=myDataBase;Initial Catalog=Test;User ID=leadmarket_user;Password=Cooki35"));
        }

        [Test]
        public void Test()
        {
            // Arrange

            // Act

            // Assert

        }
    }
}
