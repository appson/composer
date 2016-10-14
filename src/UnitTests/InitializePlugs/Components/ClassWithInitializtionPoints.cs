namespace Appson.Composer.UnitTests.InitializePlugs.Components
{
	public class ClassWithInitializtionPoints
	{
		[ComponentPlug]
		public ISampleContract SampleContract { get; set; }

		[ConfigurationPoint("InitPointVariable")]
		public int InitPoint { get; set; }
	}
}
