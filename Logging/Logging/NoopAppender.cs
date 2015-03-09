using log4net.Appender;
using log4net.Core;

namespace Logging
{
	public class NoopAppender: AppenderSkeleton
	{
		protected override void Append(LoggingEvent loggingEvent)
		{}

		public object Token { get; set; }
	}
}