using log4net.Appender;
using log4net.Core;

namespace Logging.XmlConfigured
{
	public sealed class FixedMemoryAppender: MemoryAppender
	{
		public FixedMemoryAppender()
		{
			Fix = FixFlags.Partial;
		}
	}
}