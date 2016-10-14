namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class MultipleCompositionConstructors
	{
		public IAnotherContract AnotherContract { get; set; }
		public ISampleContract SampleContract { get; set; }

		[CompositionConstructor]
		public MultipleCompositionConstructors(ISampleContract sampleContract)
		{
			SampleContract = sampleContract;
		}

		[CompositionConstructor]
		public MultipleCompositionConstructors(IAnotherContract anotherContract)
		{
			AnotherContract = anotherContract;
		}
	}
}
