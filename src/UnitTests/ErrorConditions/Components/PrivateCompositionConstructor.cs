namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class PrivateCompositionConstructor
	{
		public ISampleContract SampleContract { get; set; }

		[CompositionConstructor]
		private PrivateCompositionConstructor(ISampleContract sampleContract)
		{
			SampleContract = sampleContract;
		}
	}
}
