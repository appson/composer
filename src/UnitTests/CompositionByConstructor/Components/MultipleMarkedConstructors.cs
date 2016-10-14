namespace Appson.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class MultipleMarkedConstructors
	{
		private readonly ISampleContractA _a;
		private readonly ISampleContractB _b;
		private readonly int _invokedConstructor;

		[CompositionConstructor]
		public MultipleMarkedConstructors(ISampleContractA a)
		{
			_a = a;
			_invokedConstructor = 2;
		}

		[CompositionConstructor]
		public MultipleMarkedConstructors(ISampleContractA a, ISampleContractB b)
		{
			_a = a;
			_b = b;
			_invokedConstructor = 3;
		}

		public ISampleContractA A
		{
			get { return _a; }
		}

		public ISampleContractB B
		{
			get { return _b; }
		}

		public int InvokedConstructor
		{
			get { return _invokedConstructor; }
		}
	}
}
