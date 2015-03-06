using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;

namespace Logging.XmlConfigured
{
	public class LoggingTests
	{
		private const string MemoryAppenderName = "MemoryAppender";
		protected MemoryAppender MemoryAppender { get; private set; }

		[TestFixtureSetUp]
		public void BootstrapLogging()
		{
			var repository = LogManager.GetRepository();
			var appenders = repository.GetAppenders();
			foreach (var appender in appenders.Where(appender => appender.Name == MemoryAppenderName))
			{
				Assert.That(MemoryAppender, Is.Null, "Multiple appender with same name");
				MemoryAppender = (MemoryAppender)appender;
			}
			Assert.That(MemoryAppender, Is.Not.Null, "Cannot find memory appender");

		}
	}
}