namespace Compositional.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class MultiConstructorsWithDefaultAndMarked
	{
		private readonly ISampleContractA _a;
		private readonly ISampleContractB _b;
		private readonly int _invokedConstructor;

		public MultiConstructorsWithDefaultAndMarked()
		{
			_a = null;
			_b = null;
			_invokedConstructor = 1;
		}

		[CompositionConstructor]
		public MultiConstructorsWithDefaultAndMarked(ISampleContractA a)
		{
			_a = a;
			_b = null;
			_invokedConstructor = 2;
		}

		public MultiConstructorsWithDefaultAndMarked(ISampleContractA a, ISampleContractB b)
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
