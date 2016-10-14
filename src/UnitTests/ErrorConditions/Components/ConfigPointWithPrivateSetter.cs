namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ConfigPointWithPrivateSetter
	{
		public ConfigPointWithPrivateSetter()
		{
			SomeConfig = null;
		}

		[ConfigurationPoint("variableName", false)]
		public string SomeConfig { get; private set; }
	}
}
