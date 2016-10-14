namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class PlugWithoutSetter
	{
		private readonly ISampleContract _sampleContract;

		public PlugWithoutSetter()
		{
			_sampleContract = null;
		}

		[ComponentPlug]
		public ISampleContract SampleContract
		{
			get
			{
				return _sampleContract;
			}
		}
	}
}
