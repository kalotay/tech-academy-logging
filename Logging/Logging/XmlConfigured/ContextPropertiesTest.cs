using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Util;

namespace Logging.XmlConfigured
{
	public abstract class ContextPropertiesTest
	{
		protected abstract ContextPropertiesBase ContextProperties { get; }

		[Test]
		public void CanSetContext()
		{
			ContextProperties["contextTest"] = "context item";
			try
			{
				LogManager.GetLogger("context").Error("context message");
				var contextAppender = LogManager.GetRepository().GetAppenders().OfType<FixedMemoryAppender>().Single();
				var events = contextAppender.GetEvents();
				Assert.That(events.Select(@event => @event.MessageObject), Has.Some.EqualTo("context message"));
				Assert.That(events.Select(@event => @event.LookupProperty("contextTest")), Has.Some.EqualTo("context item"));
			}
			finally
			{
				ContextProperties["contextTest"] = null;
			}
		}


		[TestFixture]
		public class GlobalContextPropertiesTest : ContextPropertiesTest
		{
			protected override ContextPropertiesBase ContextProperties
			{
				get { return GlobalContext.Properties; }
			}
		}

		[TestFixture]
		public class ThreadContextPropertiesTest : ContextPropertiesTest
		{
			protected override ContextPropertiesBase ContextProperties
			{
				get { return ThreadContext.Properties; }
			}
		}

		[TestFixture]
		public class LogicalThreadContextPropertiesTest : ContextPropertiesTest
		{
			protected override ContextPropertiesBase ContextProperties
			{
				get { return LogicalThreadContext.Properties; }
			}
		}

	}
}