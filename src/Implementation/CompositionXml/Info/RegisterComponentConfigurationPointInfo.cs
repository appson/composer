using System.Xml;
using System.Xml.Serialization;
using Compositional.Composer.CompositionXml.ValueParser;


namespace Compositional.Composer.CompositionXml.Info
{
	public class RegisterComponentConfigurationPointInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAnyAttribute]
		public XmlAttribute[] XAttributes { get; set; }

		[XmlAnyElement]
		public XmlElement[] XElements { get; set; }

		public void Validate()
		{
			if (Name == null)
				throw new CompositionXmlValidationException(
					"'ConfigurationPoint' element nested in a 'RegisterComponent' element, requires a 'name' attribute.");

			if (XAttributes == null)
				XAttributes = new XmlAttribute[0];

			if (XElements == null)
				XElements = new XmlElement[0];

			ValidatorUtil.ValidateXmlValueElement(XAttributes, XElements,
			                                      "'ConfigurationPoint' element nested in a 'RegisterComponent' element.");
		}
	}
}