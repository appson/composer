using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class RemoveVariableInfo : CompositionCommandInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		public override void Validate()
		{
			if (Name == null)
				throw new CompositionXmlValidationException("Name is not specified.");
		}

		public override string ToString()
		{
			return "RemoveVariable(" + Name + ")";
		}
	}
}