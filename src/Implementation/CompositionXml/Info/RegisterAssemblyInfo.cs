using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class RegisterAssemblyInfo : CompositionCommandInfo
	{
		[XmlAttribute("fullName")]
		public string FullName { get; set; }

		public override void Validate()
		{
			if (FullName == null)
				throw new CompositionXmlValidationException("Full name is not specified for 'RegisterAssembly' tag.");
		}

		public override string ToString()
		{
			return "RegisterAssembly(" + FullName + ")";
		}
	}
}