namespace Compositional.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class PrivateMarkedConstructor
	{
		private readonly ISampleContractA _a;
		private readonly ISampleContractB _b;
		private readonly int _invokedConstructor;

// ReSharper disable UnusedMember.Local
		[CompositionConstructor]
		private PrivateMarkedConstructor(ISampleContractA a, ISampleContractB b)
		{
			_a = a;
			_b = b;
			_invokedConstructor = 3;
		}
// ReSharper restore UnusedMember.Local

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
