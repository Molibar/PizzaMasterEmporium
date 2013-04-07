using System.Collections.Generic;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Caching;
using PizzaMasterEmporium.Framework.Logging;
using Rhino.Mocks;

namespace PizzaMasterEmporium.Framework.Tests.Caching
{
    [TestFixture]
    public class CacheTest
    {
        private Cache _cache;
        private ILogger _logger;
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _logger = _mockRepository.DynamicMock<ILogger>();
            Cache.DisabledDueToTesting = true;
            _cache = new Cache(_logger);
        }

        [Test]
        public void Should_()
        {
            // Arrange
            var cachingArguments = new CacheArguments
                                       {
                                           Key = "key"
                                       };
            var myQuery = new MyQuery();
            var color = "blue";

            // Act
            var result = _cache.Get(
                cachingArguments,
                () => myQuery.QueryDataBase(color, 3, true));

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(color));
        }


        public IList<string> CachingFunction(string str, int i)
        {
            var methodBase = GetType().GetMethod("CachingFunction");
            var cachingArgument = new CacheArguments
                                      {
                                      };
            //_cache.OnInvocation();
            return new[] {"a", "b", "c"};
        }
    }


    public class MyQuery
    {
        public AdvancedObject QueryDataBase(string color, int number, bool ninja)
        {
            return new AdvancedObject
                       {
                           Name = color
                       };
        }
    }

    public class AdvancedObject
    {
        public string Name { get; set; }
    }
}
