using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Core;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class XmlConfigurationTests : LoggingTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(XmlConfigurationTests));

		[Test]
		public void LogsFatal()
		{
			Assert.That(Log.IsFatalEnabled, "Fatal level disabled");
			Log.Fatal("Fatal");
			var events = MemoryAppender.GetEvents();
			var fatalEvents = events.Where(e => e.Level == Level.Fatal);
			Assert.That(fatalEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Fatal"));
		}

		[Test]
		public void LogsError()
		{
			Assert.That(Log.IsErrorEnabled, "Error level disabled");
			Log.Error("Error");
			var events = MemoryAppender.GetEvents();
			var errorEvents = events.Where(e => e.Level == Level.Error);
			Assert.That(errorEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Error"));
		}

		[Test]
		public void LogsWarn()
		{
			Assert.That(Log.IsWarnEnabled, "Warn level disabled");
			Log.Warn("Warn");
			var events = MemoryAppender.GetEvents();
			var warnEvents = events.Where(e => e.Level == Level.Warn);
			Assert.That(warnEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Warn"));
		}

		[Test]
		public void LogsInfo()
		{
			Assert.That(Log.IsInfoEnabled, "Info level disabled");
			Log.Info("Info");
			var events = MemoryAppender.GetEvents();
			var infoEvents = events.Where(e => e.Level == Level.Info);
			Assert.That(infoEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Info"));
		}

		[Test]
		public void LogsDebug()
		{
			Assert.That(Log.IsDebugEnabled, "Debug level disabled");
			Log.Debug("Debug");
			var events = MemoryAppender.GetEvents();
			var debugEvents = events.Where(e => e.Level == Level.Debug);
			Assert.That(debugEvents.Select(e => e.MessageObject), Has.Some.EqualTo("Debug"));
		}

	}
}