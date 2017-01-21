using System.Configuration;

namespace Appson.Composer.Web.Configuration
{
	public class SetupCompositionXml : ConfigurationElement
	{
		[ConfigurationProperty("key", IsRequired = true)]
		public string Key
		{
			get { return (string) this["key"]; }
			set { this["key"] = value; }
		}

		[ConfigurationProperty("assemblyName", DefaultValue = "", IsRequired = false)]
		public string AssemblyName
		{
			get { return (string) this["assemblyName"]; }
			set { this["assemblyName"] = value; }
		}

		[ConfigurationProperty("manifestResourceName", DefaultValue = "", IsRequired = false)]
		public string ManifestResourceName
		{
			get { return (string) this["manifestResourceName"]; }
			set { this["manifestResourceName"] = value; }
		}

		[ConfigurationProperty("path", DefaultValue = "", IsRequired = false)]
		public string Path
		{
			get { return (string) this["path"]; }
			set { this["path"] = value; }
		}
	}
}