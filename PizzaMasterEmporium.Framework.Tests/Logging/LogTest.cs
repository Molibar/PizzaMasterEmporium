using System;
using NUnit.Framework;
using PizzaMasterEmporium.Framework.Logging;
using Rhino.Mocks;
using log4net;

namespace PizzaMasterEmporium.Framework.Tests.Logging
{
    [TestFixture]
    class LogTest
    {
        private MockRepository _mock;
        private ILog _log;

        [SetUp]
        public void SetUp()
        {
            _mock = new MockRepository();
            _log = _mock.StrictMock<ILog>();
        }

        [Test]
        public void ShouldAssertThatOneCanOverrideThe_ILog_OfThe_Log_Class()
        {
            // Arrange
            var type = typeof(LogTest);
            new Log(_log);

            const bool enabled = true;

            const string firstMessage = "I'm logging a debug message";
            const string secondMessage = "I'm logging an info message";
            const string thirdMessage = "I'm logging an warn message";
            const string fourthMessage = "I'm logging an error message";
            const string fifthMessage = "I'm logging an fatal message";

            var secondException = new Exception("Info");
            var fourthException = new Exception("Error");

            _log.Expect(x => x.IsDebugEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsInfoEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsWarnEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsErrorEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsFatalEnabled).Return(enabled).Repeat.Once();

            _log.Expect(x => x.Debug(firstMessage, null)).Repeat.Once();
            _log.Expect(x => x.Info(secondMessage, secondException)).Repeat.Once();
            _log.Expect(x => x.Warn(thirdMessage, null)).Repeat.Once();
            _log.Expect(x => x.Error(fourthMessage, fourthException)).Repeat.Once();
            _log.Expect(x => x.Fatal(fifthMessage, null)).Repeat.Once();
            _mock.ReplayAll();

            // Act
            Log.DebugMessage(type, null, firstMessage);
            Log.InfoMessage(type, secondException, secondMessage);
            Log.WarnMessage(type, null, thirdMessage);
            Log.ErrorMessage(type, fourthException, fourthMessage);
            Log.FatalMessage(type, null, fifthMessage);

            // Assert
            _log.VerifyAllExpectations();
        }

        [Test]
        public void ShouldAssertThatNoLoggingWillBeDoneIf_LoggingIsDisabled()
        {
            // Arrange
            const bool enabled = false;
            var type = typeof(LogTest);
            new Log(_log);


            const string firstMessage = "I'm logging a debug message";
            const string secondMessage = "I'm logging an info message";
            const string thirdMessage = "I'm logging an warn message";
            const string fourthMessage = "I'm logging an error message";
            const string fifthMessage = "I'm logging an fatal message";

            var secondException = new Exception("Info");
            var fourthException = new Exception("Error");

            _log.Expect(x => x.IsDebugEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsInfoEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsWarnEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsErrorEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsFatalEnabled).Return(enabled).Repeat.Once();

            _log.Expect(x => x.Debug(null, null)).IgnoreArguments().Repeat.Never();
            _log.Expect(x => x.Info(null, null)).IgnoreArguments().Repeat.Never();
            _log.Expect(x => x.Warn(null, null)).IgnoreArguments().Repeat.Never();
            _log.Expect(x => x.Error(null, null)).IgnoreArguments().Repeat.Never();
            _log.Expect(x => x.Fatal(null, null)).IgnoreArguments().Repeat.Never();
            _mock.ReplayAll();

            // Act
            Log.DebugMessage(type, null, firstMessage);
            Log.InfoMessage(type, secondException, secondMessage);
            Log.WarnMessage(type, null, thirdMessage);
            Log.ErrorMessage(type, fourthException, fourthMessage);
            Log.FatalMessage(type, null, fifthMessage);

            // Assert
            _log.VerifyAllExpectations();
        }

        [Test]
        public void Should_assert_that_optional_arguments_is_integrated_into_the_message()
        {
            // Arrange
            const bool enabled = true;
            var type = typeof(LogTest);
            new Log(_log);
            var msg = "I love {0}!";

            _log.Expect(x => x.IsDebugEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsInfoEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsWarnEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsErrorEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsFatalEnabled).Return(enabled).Repeat.Once();

            _log.Expect(x => x.Debug("I love me!", null)).Repeat.Once();
            _log.Expect(x => x.Info("I love you!", null)).Repeat.Once();
            _log.Expect(x => x.Warn("I love him!", null)).Repeat.Once();
            _log.Expect(x => x.Error("I love her!", null)).Repeat.Once();
            _log.Expect(x => x.Fatal("I love it!", null)).Repeat.Once();
            _mock.ReplayAll();

            // Act
            Log.DebugMessage(type, msg, "me");
            Log.InfoMessage(type, msg, "you");
            Log.WarnMessage(type, msg, "him");
            Log.ErrorMessage(type, msg, "her");
            Log.FatalMessage(type, msg, "it");

            // Assert
            _log.VerifyAllExpectations();
        }

        [Test]
        public void Should_assert_that_optional_arguments_is_integrated_into_the_message_when_exception_is_sent()
        {
            // Arrange
            const bool enabled = true;
            var type = typeof(LogTest);
            new Log(_log);
            var msg = "I love {0}!";

            var exception = new Exception("Exception");

            _log.Expect(x => x.IsDebugEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsInfoEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsWarnEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsErrorEnabled).Return(enabled).Repeat.Once();
            _log.Expect(x => x.IsFatalEnabled).Return(enabled).Repeat.Once();

            _log.Expect(x => x.Debug("I love me!", exception)).Repeat.Once();
            _log.Expect(x => x.Info("I love you!", exception)).Repeat.Once();
            _log.Expect(x => x.Warn("I love him!", exception)).Repeat.Once();
            _log.Expect(x => x.Error("I love her!", exception)).Repeat.Once();
            _log.Expect(x => x.Fatal("I love it!", exception)).Repeat.Once();
            _mock.ReplayAll();

            // Act
            Log.DebugMessage(type, exception, msg, "me");
            Log.InfoMessage(type, exception, msg, "you");
            Log.WarnMessage(type, exception, msg, "him");
            Log.ErrorMessage(type, exception, msg, "her");
            Log.FatalMessage(type, exception, msg, "it");

            // Assert
            _log.VerifyAllExpectations();
        }
    }
}
