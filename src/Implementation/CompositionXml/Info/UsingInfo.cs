using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class UsingInfo : CompositionCommandInfo
	{
		[XmlAttribute("namespace")]
		public string Namespace { get; set; }

		public override void Validate()
		{
			if (Namespace == null)
				throw new CompositionXmlValidationException("Namespace is not specified.");
		}


		public override string ToString()
		{
			return "Using(" + Namespace + ")";
		}
	}
}