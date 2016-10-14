using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

namespace Appson.Composer.Factories.Remote
{
	public class CustomDataContractSerializerOperationBehavior : DataContractSerializerOperationBehavior
	{
		private List<Type> _knownTypes;

		public List<Type> KnownTypes
		{
			get { return _knownTypes; }
			set { _knownTypes = value; }
		}

		public CustomDataContractSerializerOperationBehavior(OperationDescription operation,
		                                                     DataContractFormatAttribute dataContractFormatAttribute)
			: base(operation, dataContractFormatAttribute)
		{
		}

		public CustomDataContractSerializerOperationBehavior(OperationDescription operation) : base(operation)
		{
		}

		public override XmlObjectSerializer CreateSerializer(
			Type type, XmlDictionaryString name, XmlDictionaryString ns,
			IList<Type> knownTypeList)
		{
			return new DataContractSerializer(type, name, ns, _knownTypes,
			                                  0x7FFF,
			                                  false,
			                                  true /* preserveObjectReferences */,
			                                  null);
		}
	}
}