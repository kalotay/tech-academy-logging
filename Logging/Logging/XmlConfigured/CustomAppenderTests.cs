using System.Linq;
using NUnit.Framework;
using log4net;

namespace Logging.XmlConfigured
{
	[TestFixture]
	public class CustomAppenderTests
	{
		[Test]
		public void CanUseCustomAppenders()
		{
			var repository = LogManager.GetRepository();
			var appenders = repository.GetAppenders();
			var customAppenders = appenders.OfType<NoopAppender>();
			var customAppender = customAppenders.First();
			Assert.That(customAppender, Is.Not.Null);
			Assert.That(customAppender.Name, Is.EqualTo("CustomAppender"));
			Assert.That(customAppender.Token, Is.EqualTo("MyToken"));
		}
	}
}