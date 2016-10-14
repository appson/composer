using System.Xml.Serialization;

namespace Compositional.Composer.CompositionXml.Info
{
	public class RegisterComponentPlugInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("refType")]
		public string RefType { get; set; }

		[XmlAttribute("refName")]
		public string RefName { get; set; }

		public void Validate()
		{
			if (Name == null)
				throw new CompositionXmlValidationException("Plug element in RegisterComponent element requires a 'name' attribute.");

			if (RefType == null)
				throw new CompositionXmlValidationException("Plug element in RegisterComponent element requires a 'refType' attribute.");
		}
	}
}