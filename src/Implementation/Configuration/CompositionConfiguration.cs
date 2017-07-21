using System.Configuration;

namespace Appson.Composer.Configuration
{
	public class CompositionConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("setupCompositionXmls", IsDefaultCollection = false)]
		public SetupCompositionXmlsCollection SetupCompositionXmls => (SetupCompositionXmlsCollection)base["setupCompositionXmls"];
	}
}