namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class NoCompositionContructors
	{
		public ISampleContract SampleContract { get; set; }
		public IAnotherContract AnotherContract { get; set; }

		public NoCompositionContructors(ISampleContract sampleContract)
		{
			SampleContract = sampleContract;
		}

		public NoCompositionContructors(ISampleContract sampleContract, IAnotherContract anotherContract)
		{
			SampleContract = sampleContract;
			AnotherContract = anotherContract;
		}
	}
}
