using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Core;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class MessageObjects: LoggingTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MessageObjects));

		[Test]
		public void ArbitraryMessagesCanBeLogged()
		{
			Assert.That(Log.IsInfoEnabled);
			Log.Info(this);
			var events = MemoryAppender.GetEvents().Where(@event => @event.Level == Level.Info);
			Assert.That(events.Select(@event => @event.MessageObject), Has.Some.EqualTo(this));
		}

		[Test]
		public void FormatStringsCanBeSpecified()
		{
			Assert.That(Log.IsInfoEnabled);
			var expectedMessage = Format(1);
			LogFormat(1);
			var events = MemoryAppender.GetEvents();
			Assert.That(events.Select(@event => @event.MessageObject.ToString()), Has.Some.EqualTo(expectedMessage));
		}

		[Test, Ignore("might only be true for disables levels")]
		public void LogFormatIsFasterThanStringFormat()
		{
			const int iterationCount = 1000;

			var logFormatStopwatch = Stopwatch.StartNew();
			for (var i = 0; i < iterationCount; i++)
			{
				LogFormat(i);
			}
			var logFormatElapsed = logFormatStopwatch.Elapsed;

			var stringFormatStopwatch = Stopwatch.StartNew();
			for (var i = 0; i < iterationCount; i++)
			{
				StringFormatWithLog(i);
			}
			var stringFormatElapsed = stringFormatStopwatch.Elapsed;

			Assert.That(logFormatElapsed, Is.LessThanOrEqualTo(stringFormatElapsed));

		}

		private static void StringFormatWithLog(object count)
		{
			var message = Format(count);
			Log.Info(message);
		}

		private static object Format(object count)
		{
			return string.Format("Message number {0} from {1}: {2}", count, typeof (MessageObjects), "hello");
		}

		private static void LogFormat(object count)
		{
			Log.InfoFormat("Message number {0} from {1}: {2}", count, typeof(MessageObjects), "hello");
		}
	}
}