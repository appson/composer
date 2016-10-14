using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class UnregisterInfo : CompositionCommandInfo
	{
		[XmlAttribute("contractType")]
		public string ContractType { get; set; }

		[XmlAttribute("contractName")]
		public string ContractName { get; set; }

		public override void Validate()
		{
			if (ContractType == null)
				throw new CompositionXmlValidationException("Unregister tag requires a 'contractType' attribute.");
		}

		public override string ToString()
		{
			var result = "Unregister(" + ContractType;

			if (ContractName != null)
				result += ", '" + ContractName + "'";
			else
				result += ", ''";

			result += ")";

			return result;
		}
	}
}