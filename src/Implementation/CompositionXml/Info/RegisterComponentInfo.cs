using System.Xml.Serialization;

namespace Compositional.Composer.CompositionXml.Info
{
	public class RegisterComponentInfo : CompositionCommandInfo
	{
		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("contractType")]
		public string ContractType { get; set; }

		[XmlAttribute("contractName")]
		public string ContractName { get; set; }

		[XmlElement("Plug", typeof (RegisterComponentPlugInfo))]
		public RegisterComponentPlugInfo[] Plugs { get; set; }

		[XmlElement("ConfigurationPoint", typeof (RegisterComponentConfigurationPointInfo))]
		public RegisterComponentConfigurationPointInfo[] ConfigurationPoints { get; set; }

		public override void Validate()
		{
			if (Type == null)
				throw new CompositionXmlValidationException("RegisterComponent tag requires a 'type' attribute.");

			if (Plugs == null)
				Plugs = new RegisterComponentPlugInfo[0];

			if (ConfigurationPoints == null)
				ConfigurationPoints = new RegisterComponentConfigurationPointInfo[0];

			foreach (var plugInfo in Plugs)
				plugInfo.Validate();

			foreach (var configurationPointInfo in ConfigurationPoints)
				configurationPointInfo.Validate();
		}

		public override string ToString()
		{
			var result = "RegisterComponent(" + Type;

			if (ContractType != null)
				result += ", " + ContractType;
			else
				result += ", *";

			if (ContractName != null)
				result += ", '" + ContractName + "'";
			else
				result += ", ''";

			result += ")";

			return result;
		}
	}
}