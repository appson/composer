using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public class RemoteComponentInfo : CompositionCommandInfo
	{
		[XmlAttribute("contractType")]
		public string ContractType { get; set; }

		[XmlAttribute("contractName")]
		public string ContractName { get; set; }

		[XmlAttribute("serverAddress")]
		public string ServerAddress { get; set; }

		[XmlAttribute("serverAddressVariableName")]
		public string ServerAddressVariableName { get; set; }

		[XmlAttribute("spnIdentity")]
		public string SpnIdentity { get; set; }

		[XmlAttribute("knownTypesVariableName")]
		public string KnownTypesVariableName { get; set; }

		[XmlAttribute("securityMode")]
		public string SecurityMode { get; set; }

		public int? MaxBufferSizeNullable { get; set; }

		public int? MaxReceivedMessageSizeNullable { get; set; }

		[XmlAttribute("maxBufferSize")]
		public int MaxBufferSize
		{
			get
			{
				return MaxBufferSizeNullable != null ? MaxBufferSizeNullable.Value : 0;
			}
			set
			{
				MaxBufferSizeNullable = value;
			}
		}

		[XmlAttribute("maxReceivedMessageSize")]
		public int MaxReceivedMessageSize
		{
			get
			{
				return MaxReceivedMessageSizeNullable != null ? MaxReceivedMessageSizeNullable.Value : 0;
			}
			set
			{
				MaxReceivedMessageSizeNullable = value;
			}
		}

		public override void Validate()
		{
			if (ContractType == null)
				throw new CompositionXmlValidationException("RemoteComponent tag requires a 'contractType' attribute.");

			if ((ServerAddress == null) && (ServerAddressVariableName == null))
				throw new CompositionXmlValidationException(
					"RemoteComponent tag requires either 'serverAddress' or 'serverAddressVariableName' attribute.");
		}

		public override string ToString()
		{
			var result = "RemoteComponent(" + ContractType;

			if (ContractName != null)
				result += ", '" + ContractName + "'";
			else
				result += ", ''";

			if (ServerAddress != null)
				result += ", '" + ServerAddress + "'";
			else
				result += ", '${" + ServerAddressVariableName + "}'";

			result += ")";

			return result;
		}
	}
}