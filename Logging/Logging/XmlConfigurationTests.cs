using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace Logging
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
			XmlConfigurator.Configure();
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

	}
}