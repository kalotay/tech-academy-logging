using NUnit.Framework;
using log4net.Config;

namespace Logging.XmlConfigured
{
	[SetUpFixture]
	public class Configurator
	{
		[SetUp]
		public void Configure()
		{
			XmlConfigurator.Configure();
		}
	}
}