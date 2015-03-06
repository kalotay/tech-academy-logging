using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class XmlConfigurationTests
	{
		private const string MemoryAppenderName = "MemoryAppender";

		private static readonly ILog Log = LogManager.GetLogger(typeof(XmlConfigurationTests));

		private MemoryAppender _memoryAppender;

		[TestFixtureSetUp]
		public void BootstrapLogging()
		{
			var repository = LogManager.GetRepository();
			var appenders = repository.GetAppenders();
			foreach (var appender in appenders.Where(appender => appender.Name == MemoryAppenderName))
			{
				Assert.That(_memoryAppender, Is.Null, "Multiple appender with same name");
				_memoryAppender = (MemoryAppender)appender;
			}
			Assert.That(_memoryAppender, Is.Not.Null, "Cannot find memory appender");

		}

		[Test]
		public void LogsFatal()
		{
			Assert.That(Log.IsFatalEnabled, "Fatal level disabled");
			Log.Fatal("Fatal");
			var events = _memoryAppender.GetEvents();
			var fatalEvents = events.Where(e => e.Level == Level.Fatal);
			Assert.That(fatalEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Fatal"));
		}

		[Test]
		public void LogsError()
		{
			Assert.That(Log.IsErrorEnabled, "Error level disabled");
			Log.Error("Error");
			var events = _memoryAppender.GetEvents();
			var errorEvents = events.Where(e => e.Level == Level.Error);
			Assert.That(errorEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Error"));
		}

		[Test]
		public void LogsWarn()
		{
			Assert.That(Log.IsWarnEnabled, "Warn level disabled");
			Log.Warn("Warn");
			var events = _memoryAppender.GetEvents();
			var warnEvents = events.Where(e => e.Level == Level.Warn);
			Assert.That(warnEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Warn"));
		}

		[Test]
		public void LogsInfo()
		{
			Assert.That(Log.IsInfoEnabled, "Info level disabled");
			Log.Info("Info");
			var events = _memoryAppender.GetEvents();
			var infoEvents = events.Where(e => e.Level == Level.Info);
			Assert.That(infoEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Info"));
		}

		[Test]
		public void LogsDebug()
		{
			Assert.That(Log.IsDebugEnabled, "Debug level disabled");
			Log.Debug("Debug");
			var events = _memoryAppender.GetEvents();
			var debugEvents = events.Where(e => e.Level == Level.Debug);
			Assert.That(debugEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Debug"));
		}

	}
}