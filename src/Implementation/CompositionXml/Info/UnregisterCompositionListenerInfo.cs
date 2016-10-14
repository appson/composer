using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class UnregisterCompositionListenerInfo : CompositionCommandInfo
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		public override void Validate()
		{
			if (Name == null)
				throw new CompositionXmlValidationException("Name is not specified for 'UnregisterCompositionListener' element.");
		}

		public override string ToString()
		{
			return "UnregisterCompositionListener(" + Name + ")";
		}
	}
}