using NUnit.Framework;
using PizzaMasterEmporium.Framework.Logging;
using PizzaMasterEmporium.Framework.Logging.ForTesting;

namespace PizzaMasterEmporium.Framework.Tests.Helpers
{
    [TestFixture]
    public class UriHelperTest
    {
        private MemoryLoggerForTest _memoryLoggerForTest;

        [SetUp]
        public void SetUp()
        {
            _memoryLoggerForTest = new MemoryLoggerForTest();
            new Log(_memoryLoggerForTest);
        }
    }
}
