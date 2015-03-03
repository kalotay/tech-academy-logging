using NUnit.Framework;
using log4net;
using log4net.Config;

namespace Logging
{
	[TestFixture]
	public class LoggingInterfaceTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (LoggingInterfaceTests));

		[TestFixtureSetUp]
		public void BootstrapLogger()
		{
			BasicConfigurator.Configure();
		}

		[Test]
		public void LogsFatal()
		{
			Assert.That(Log.IsFatalEnabled, "Fatal level disabled");
			Log.Fatal("Fatal");
		}

		[Test]
		public void LogsError()
		{
			Assert.That(Log.IsErrorEnabled, "Error level disabled");
			Log.Error("Error");
		}

		[Test]
		public void LogsWarn()
		{
			Assert.That(Log.IsWarnEnabled, "Warn level disabled");
			Log.Warn("Warn");
		}

		[Test]
		public void LogsInfo()
		{
			Assert.That(Log.IsInfoEnabled, "Info level disabled");
			Log.Info("Info");
		}

		[Test]
		public void LogsDebug()
		{
			Assert.That(Log.IsDebugEnabled, "Debug level disabled");
			Log.Debug("Debug");
		}

	}
}