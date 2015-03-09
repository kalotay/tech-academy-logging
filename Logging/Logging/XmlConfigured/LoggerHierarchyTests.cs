using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;

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

		[Test]
		public void AppenderInheritance()
		{
			var repository = LogManager.GetRepository();
			var appenders = repository.GetAppenders();
			var memoryAppenders = appenders.OfType<MemoryAppender>().ToArray();
			var rootAppender = memoryAppenders.Single(appender => appender.Name == "MemoryAppender");
			var xAppender = memoryAppenders.Single(appender => appender.Name == "XAppender");
			var zAppender = memoryAppenders.Single(appender => appender.Name == "ZAppender");
			var securityAppender = memoryAppenders.Single(appender => appender.Name == "SecurityAppender");

			LogManager.GetLogger("").Error("rootMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("rootMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("rootMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("rootMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("rootMessage"));

			LogManager.GetLogger("X").Error("xMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("xMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("xMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("xMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("xMessage"));

			LogManager.GetLogger("X.Y").Error("yMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("yMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("yMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("yMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("yMessage"));

			LogManager.GetLogger("X.Y.Z").Error("zMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("zMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("zMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("zMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("zMessage"));

			LogManager.GetLogger("security").Error("securityMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("securityMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("securityMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("securityMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("securityMessage"));

			LogManager.GetLogger("security.other").Error("otherMessage");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("otherMessage"));
			Assert.That(xAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("otherMessage"));
			Assert.That(zAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("otherMessage"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("otherMessage"));
		}
	}
}