using System;
using System.Configuration;

namespace Appson.Composer.Web.Configuration
{
	public class CompositionConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("setResolver", DefaultValue = "true", IsRequired = false)]
		public Boolean SetResolver
		{
			get { return (Boolean) this["setResolver"]; }
			set { this["setResolver"] = value; }
		}

		[ConfigurationProperty("registerDefaultComponents", DefaultValue = "true", IsRequired = false)]
		public Boolean RegisterDefaultComponents
		{
			get { return (Boolean) this["registerDefaultComponents"]; }
			set { this["registerDefaultComponents"] = value; }
		}

		[ConfigurationProperty("setupCompositionXmls", IsDefaultCollection = false)]
		public SetupCompositionXmlsCollection SetupCompositionXmls => (SetupCompositionXmlsCollection)base["setupCompositionXmls"];
	}
}