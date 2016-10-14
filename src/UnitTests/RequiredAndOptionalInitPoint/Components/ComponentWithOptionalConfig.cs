namespace Appson.Composer.UnitTests.RequiredAndOptionalInitPoint.Components
{
	[Contract]
	[Component]
	public class ComponentWithOptionalConfig
	{
		[ConfigurationPoint(false)]
		public string SomeConfig { get; set; }
	}
}
