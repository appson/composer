using System.Xml.Serialization;

namespace Appson.Composer.CompositionXml.Info
{
	public abstract class CompositionCommandInfo
	{
		[XmlAttribute("ignoreOnError")]
		public bool IgnoreOnError { get; set; }

		public abstract void Validate();


		public override string ToString()
		{
			return "CompositionCommandInfo(" + GetType().Name + ")";
		}
	}
}