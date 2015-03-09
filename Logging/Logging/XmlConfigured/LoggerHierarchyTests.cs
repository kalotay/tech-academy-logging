using NUnit.Framework;
using log4net;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class LoggerHierarchyTests
	{
		[Test]
		public void LoggerByNameAndType()
		{
			var loggerByName = LogManager.GetLogger("Logging.XmlConfigured.LoggerHierarchyTests");
			var loggerByType = LogManager.GetLogger(typeof(LoggerHierarchyTests));
			Assert.That(loggerByName, Is.EqualTo(loggerByType));
		}

		[Test]
		public void LoggerLevelHierarchy()
		{
			var rootLogger = LogManager.GetLogger("");
			var aLogger = LogManager.GetLogger("A");
			var xLogger = LogManager.GetLogger("X");
			var yLogger = LogManager.GetLogger("X.Y");
			var zLogger = LogManager.GetLogger("X.Y.Z");
			var wLogger = LogManager.GetLogger("X.Y.W");

			Assert.That(rootLogger.IsFatalEnabled);
			Assert.That(rootLogger.IsErrorEnabled);
			Assert.That(rootLogger.IsWarnEnabled);
			Assert.That(rootLogger.IsInfoEnabled);
			Assert.That(rootLogger.IsDebugEnabled);

			Assert.That(aLogger.IsFatalEnabled);
			Assert.That(aLogger.IsErrorEnabled);
			Assert.That(aLogger.IsWarnEnabled);
			Assert.That(aLogger.IsInfoEnabled);
			Assert.That(aLogger.IsDebugEnabled);

			Assert.That(xLogger.IsFatalEnabled);
			Assert.That(xLogger.IsErrorEnabled);
			Assert.That(xLogger.IsWarnEnabled);
			Assert.That(xLogger.IsInfoEnabled, Is.False);
			Assert.That(xLogger.IsDebugEnabled, Is.False);

			Assert.That(yLogger.IsFatalEnabled);
			Assert.That(yLogger.IsErrorEnabled);
			Assert.That(yLogger.IsWarnEnabled);
			Assert.That(yLogger.IsInfoEnabled, Is.False);
			Assert.That(yLogger.IsDebugEnabled, Is.False);

			Assert.That(zLogger.IsFatalEnabled);
			Assert.That(zLogger.IsErrorEnabled);
			Assert.That(zLogger.IsWarnEnabled, Is.False);
			Assert.That(zLogger.IsInfoEnabled, Is.False);
			Assert.That(zLogger.IsDebugEnabled, Is.False);

			Assert.That(wLogger.IsFatalEnabled);
			Assert.That(wLogger.IsErrorEnabled);
			Assert.That(wLogger.IsWarnEnabled);
			Assert.That(wLogger.IsInfoEnabled);
			Assert.That(wLogger.IsDebugEnabled, Is.False);
		}
	}
}