using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Util;

namespace Logging.XmlConfigured
{
	public abstract class ContextStackTest
	{
		protected abstract ThreadContextStacks ContextStacks { get; }

		[Test]
		public void CanUseStacks()
		{
			var stack = ContextStacks["simple"];
			Assert.That(stack.Count, Is.EqualTo(0));
			using (stack.Push("push"))
			{
				LogManager.GetLogger("context").Error("simple push");
				Assert.That(stack.Count, Is.EqualTo(1));
			}
			Assert.That(stack.Count, Is.EqualTo(0));
			var contextAppender = LogManager.GetRepository().GetAppenders().OfType<FixedMemoryAppender>().Single();
			var events = contextAppender.GetEvents();
			Assert.That(events.Select(@event => @event.MessageObject), Has.Some.EqualTo("simple push"));
			Assert.That(events.Select(@event => @event.LookupProperty("simple")), Has.Some.EqualTo("push"));
		}

		[Test]
		public void CanUseStacksWithMultipleItems()
		{
			var stack = ContextStacks["stacked"];
			Assert.That(stack.Count, Is.EqualTo(0));
			using (stack.Push("push 1"))
			{
				LogManager.GetLogger("context").Error("stacked push 1");
				Assert.That(stack.Count, Is.EqualTo(1));
				using (stack.Push("push 2"))
				{
					LogManager.GetLogger("context").Error("stacked push 2");
					Assert.That(stack.Count, Is.EqualTo(2));
				}
			}
			Assert.That(stack.Count, Is.EqualTo(0));
			var contextAppender = LogManager.GetRepository().GetAppenders().OfType<FixedMemoryAppender>().Single();
			var events = contextAppender.GetEvents();
			Assert.That(events.Select(@event => @event.MessageObject), Has.Some.EqualTo("stacked push 1"));
			Assert.That(events.Select(@event => @event.MessageObject), Has.Some.EqualTo("stacked push 2"));
			Assert.That(events.Select(@event => @event.LookupProperty("stacked")), Has.Some.EqualTo("push 1"));
			Assert.That(events.Select(@event => @event.LookupProperty("stacked")), Has.Some.StringContaining("push 2"));
		}

		[TestFixture]
		public class ThreadContextStackTest: ContextStackTest
		{
			protected override ThreadContextStacks ContextStacks
			{
				get { return ThreadContext.Stacks; }
			}
		}

		[TestFixture]
		public class LogicalThreadContextStackTest: ContextStackTest
		{
			protected override ThreadContextStacks ContextStacks
			{
				get { return LogicalThreadContext.Stacks; }
			}
		}
	}

}