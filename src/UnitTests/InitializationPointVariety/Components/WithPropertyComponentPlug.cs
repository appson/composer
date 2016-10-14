namespace Appson.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithPropertyComponentPlug
	{
		[ComponentPlug]
		public ISampleContract SampleContract { get; set; }
	}
}
