namespace Appson.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithFieldConfigurationPoint
	{
		[ConfigurationPoint("SomeConfigurationPoint")]
		public string SomeConfigurationPoint;
	}
}
