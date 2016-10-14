using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class IncludeInfo : CompositionCommandInfo
	{
		[XmlAttribute("path")]
		public string Path { get; set; }

		[XmlAttribute("manifestResourceName")]
		public string ManifestResourceName { get; set; }

		[XmlAttribute("assemblyName")]
		public string AssemblyName { get; set; }

		public override void Validate()
		{
			if (Path != null)
			{
				if ((ManifestResourceName != null) || (AssemblyName != null))
					throw new CompositionXmlValidationException(
						"When XML path is specified, 'manifestResourceName' and 'assemblyName' attributes should not be specified. Path: " +
						Path);
			}
			else
			{
				if ((ManifestResourceName == null) || (AssemblyName == null))
					throw new CompositionXmlValidationException(
						"When XML path is not specified, both 'manifestResourceName' and 'assemblyName' attributes should be provided.");
			}
		}

		public override string ToString()
		{
			if (Path != null)
				return "Include(" + Path + ")";

			return "Include(" + ManifestResourceName + ", " + AssemblyName + ")";
		}
	}
}