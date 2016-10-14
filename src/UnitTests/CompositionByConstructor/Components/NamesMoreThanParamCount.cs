namespace Appson.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class NamesMoreThanParamCount
	{
		private readonly ISampleContractA _defaultA;
		private readonly ISampleContractB _defaultB;
		private readonly ISampleContractA _namedA;
		private readonly ISampleContractB _namedB;

		[CompositionConstructor(null, "someName", null, "someName", "someExtraName")]
		public NamesMoreThanParamCount(ISampleContractA defaultA, ISampleContractA namedA, ISampleContractB defaultB, ISampleContractB namedB)
		{
			_defaultA = defaultA;
			_namedA = namedA;

			_defaultB = defaultB;
			_namedB = namedB;
		}

		public ISampleContractA DefaultA
		{
			get { return _defaultA; }
		}

		public ISampleContractB DefaultB
		{
			get { return _defaultB; }
		}

		public ISampleContractA NamedA
		{
			get { return _namedA; }
		}

		public ISampleContractB NamedB
		{
			get { return _namedB; }
		}
	}
}
