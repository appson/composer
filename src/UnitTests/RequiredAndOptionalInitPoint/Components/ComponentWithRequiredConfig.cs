namespace Appson.Composer.UnitTests.RequiredAndOptionalInitPoint.Components
{
	[Contract]
	[Component]
	public class ComponentWithRequiredConfig
	{
		[ConfigurationPoint(true)]
		public string SomeConfig { get; set; }
	}
}
