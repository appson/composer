using System.Xml;
using System.Xml.Serialization;
using Compositional.Composer.CompositionXml.ValueParser;

namespace Compositional.Composer.CompositionXml.Info
{
	public class RegisterCompositionListenerInfo : CompositionCommandInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAnyAttribute]
		public XmlAttribute[] XAttributes { get; set; }

		[XmlAnyElement]
		public XmlElement[] XElements { get; set; }

		public override void Validate()
		{
			if (Name == null)
				throw new CompositionXmlValidationException(
					"Name attribute is not specified. Name is required for registering composition listeners.");

			if (XAttributes == null)
				XAttributes = new XmlAttribute[0];

			if (XElements == null)
				XElements = new XmlElement[0];

			ValidatorUtil.ValidateXmlValueElement(XAttributes, XElements, "Registering composition listener");
		}

		public override string ToString()
		{
			return "RegisterCompositionListener(" + Name + ")";
		}
	}
}