using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	[XmlRoot(Namespace = "http://www.compositional.net/schema/compositionXml.1.0.xsd")]
	public class CompositionInfo
	{
		[XmlElement("Using", typeof(UsingInfo)),
		 XmlElement("UsingAssembly", typeof(UsingAssemblyInfo)),
		 XmlElement("SetVariable", typeof(SetVariableInfo)),
		 XmlElement("RemoveVariable", typeof(RemoveVariableInfo)),
		 XmlElement("RegisterCompositionListener", typeof(RegisterCompositionListenerInfo)),
		 XmlElement("UnregisterCompositionListener", typeof(UnregisterCompositionListenerInfo)),
		 XmlElement("RegisterComponent", typeof(RegisterComponentInfo)),
		 XmlElement("RemoteComponent", typeof(RemoteComponentInfo)),
		 XmlElement("Unregister", typeof(UnregisterInfo)),
		 XmlElement("UnregisterFamily", typeof(UnregisterFamilyInfo)),
		 XmlElement("RegisterAssembly", typeof(RegisterAssemblyInfo)),
		 XmlElement("Include", typeof(IncludeInfo))]
		public CompositionCommandInfo[] CommandInfos { get; set; }

		public void Validate()
		{
			if (CommandInfos == null)
				return;

			foreach (var info in CommandInfos)
			{
				info.Validate();
			}
		}
	}
}