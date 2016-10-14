using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class UnregisterFamilyInfo : CompositionCommandInfo
	{
		[XmlAttribute("contractType")]
		public string ContractType { get; set; }

		public override void Validate()
		{
			if (ContractType == null)
				throw new CompositionXmlValidationException("UnregisterFamily tag requires a 'contractType' attribute.");
		}

		public override string ToString()
		{
			return "UnregisterFamily(" + ContractType + ")";
		}
	}
}