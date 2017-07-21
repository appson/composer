using System.Configuration;

namespace Appson.Composer.Configuration
{
	public class SetupCompositionXmlsCollection : ConfigurationElementCollection
	{
		#region Overrides of ConfigurationElementCollection

		public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

	    protected override ConfigurationElement CreateNewElement()
		{
			return new SetupCompositionXml();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SetupCompositionXml) element).Key;
		}

		#endregion
	}
}