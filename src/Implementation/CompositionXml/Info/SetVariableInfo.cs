using System.Xml;
using System.Xml.Serialization;
using Appson.Composer.CompositionXml.ValueParser;

namespace Appson.Composer.CompositionXml.Info
{
	public class SetVariableInfo : CompositionCommandInfo
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
				throw new CompositionXmlValidationException("Name attribute is not specified. Name is required for setting variables.");

			if (XAttributes == null)
				XAttributes = new XmlAttribute[0];

			if (XElements == null)
				XElements = new XmlElement[0];

			ValidatorUtil.ValidateXmlValueElement(XAttributes, XElements, "Setting variable with name: " + Name);
		}

		public override string ToString()
		{
			return "SetVariable(" + Name + ")";
		}
	}
}