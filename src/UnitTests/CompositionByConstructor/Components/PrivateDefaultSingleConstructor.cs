namespace Compositional.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class PrivateDefaultSingleConstructor
	{
		private readonly ISampleContractA _a;
		private readonly ISampleContractB _b;
		private readonly int _invokedConstructor;

// ReSharper disable UnusedMember.Local
		private PrivateDefaultSingleConstructor()
		{
			_a = null;
			_b = null;
			_invokedConstructor = 1;
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
