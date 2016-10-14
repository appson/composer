namespace Appson.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class NamesLessThanParamCount
	{
		private readonly ISampleContractA _defaultA;
		private readonly ISampleContractB _defaultB;
		private readonly ISampleContractA _namedA;
		private readonly ISampleContractB _unnamedB;

		[CompositionConstructor(null, "someName")]
		public NamesLessThanParamCount(ISampleContractA defaultA, ISampleContractA namedA, ISampleContractB defaultB, ISampleContractB unnamedB)
		{
			_defaultA = defaultA;
			_namedA = namedA;

			_defaultB = defaultB;
			_unnamedB = unnamedB;
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

		public ISampleContractB UnnamedB
		{
			get { return _unnamedB; }
		}
	}
}
