using NUnit.Framework;
using log4net;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class LoggerLevelThresholds
	{
		[Test]
		public void FatalLoggerOnlyHasFatalEnabled()
		{
			var log = LogManager.GetLogger("FatalLogger");
			Assert.That(log.IsFatalEnabled, "Fatal should be enabled");
			Assert.That(log.IsErrorEnabled, Is.False, "Error should be disabled");
			Assert.That(log.IsWarnEnabled, Is.False, "Warn should be disabled");
			Assert.That(log.IsInfoEnabled, Is.False, "Info should be disabled");
			Assert.That(log.IsDebugEnabled, Is.False, "Debug should be disabled");
		}

		[Test]
		public void ErrorLoggerOnlyHasErrorEnabled()
		{
			var log = LogManager.GetLogger("ErrorLogger");
			Assert.That(log.IsFatalEnabled, "Fatal should be enabled");
			Assert.That(log.IsErrorEnabled, "Error should be enabled");
			Assert.That(log.IsWarnEnabled, Is.False, "Warn should be disabled");
			Assert.That(log.IsInfoEnabled, Is.False, "Info should be disabled");
			Assert.That(log.IsDebugEnabled, Is.False, "Debug should be disabled");
		}

		[Test]
		public void WarnLoggerOnlyHasWarnEnabled()
		{
			var log = LogManager.GetLogger("WarnLogger");
			Assert.That(log.IsFatalEnabled, "Fatal should be enabled");
			Assert.That(log.IsErrorEnabled, "Error should be enabled");
			Assert.That(log.IsWarnEnabled, "Warn should be enabled");
			Assert.That(log.IsInfoEnabled, Is.False, "Info should be disabled");
			Assert.That(log.IsDebugEnabled, Is.False, "Debug should be disabled");
		}

		[Test]
		public void InfoLoggerOnlyHasInfoEnabled()
		{
			var log = LogManager.GetLogger("InfoLogger");
			Assert.That(log.IsFatalEnabled, "Fatal should be enabled");
			Assert.That(log.IsErrorEnabled, "Error should be enabled");
			Assert.That(log.IsWarnEnabled, "Warn should be enabled");
			Assert.That(log.IsInfoEnabled, "Info should be enabled");
			Assert.That(log.IsDebugEnabled, Is.False, "Debug should be disabled");
		}

		[Test]
		public void DebugLoggerOnlyHasDebugEnabled()
		{
			var log = LogManager.GetLogger("DebugLogger");
			Assert.That(log.IsFatalEnabled, "Fatal should be enabled");
			Assert.That(log.IsErrorEnabled, "Error should be enabled");
			Assert.That(log.IsWarnEnabled, "Warn should be enabled");
			Assert.That(log.IsInfoEnabled, "Info should be enabled");
			Assert.That(log.IsDebugEnabled, "Debug should be enabled");
		}
	}
}