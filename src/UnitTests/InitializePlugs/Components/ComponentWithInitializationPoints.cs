namespace Compositional.Composer.UnitTests.InitializePlugs.Components
{
	[Contract]
	[Component]
	public class ComponentWithInitializationPoints
	{
		[ComponentPlug]
		public ISampleContract SampleContract { get; set; }

		[ConfigurationPoint("InitPointVariable")]
		public int InitPoint { get; set; }
	}
}
