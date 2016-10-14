namespace Appson.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithPropertyConfigurationPoint
	{
		[ConfigurationPoint("SomeConfigurationPoint")]
		public string SomeConfigurationPoint { get; set; }
	}
}
