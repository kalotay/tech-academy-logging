using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class MisconfigurationTests
	{
		[Test]
		public void MisnamedLogger()
		{
			LogManager.GetLogger("securtiy").Error("sensitive stuff");
			var memoryAppenders = LogManager.GetRepository().GetAppenders().OfType<MemoryAppender>().ToArray();
			var rootAppender = memoryAppenders.First(appender => appender.Name == "MemoryAppender");
			var securityAppender = memoryAppenders.First(appender => appender.Name == "SecurityAppender");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.None.EqualTo("sensitive stuff"));
			Assert.That(securityAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("sensitive stuff"));
		}

		[Test]
		public void MisnamedAppender()
		{
			LogManager.GetLogger("special").Error("special stuff");
			var memoryAppenders = LogManager.GetRepository().GetAppenders().OfType<MemoryAppender>().ToArray();
			var rootAppender = memoryAppenders.First(appender => appender.Name == "MemoryAppender");
			var special = memoryAppenders.First(appender => appender.Name == "SpecialAppender");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("sensitive stuff"));
			Assert.That(special.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("sensitive stuff"));
		}

		[Test]
		public void MisnamedAppenderType()
		{
			LogManager.GetLogger("special").Error("extra stuff");
			var memoryAppenders = LogManager.GetRepository().GetAppenders().OfType<MemoryAppender>().ToArray();
			var rootAppender = memoryAppenders.First(appender => appender.Name == "MemoryAppender");
			var special = memoryAppenders.First(appender => appender.Name == "ExtraAppender");
			Assert.That(rootAppender.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("sensitive stuff"));
			Assert.That(special.GetEvents().Select(@event => @event.MessageObject), Has.Some.EqualTo("sensitive stuff"));
		}
	}
}