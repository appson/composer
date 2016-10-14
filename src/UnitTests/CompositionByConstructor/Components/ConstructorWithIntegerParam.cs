namespace Appson.Composer.UnitTests.CompositionByConstructor.Components
{
	[Contract]
	[Component]
	public class ConstructorWithIntegerParam
	{
		[CompositionConstructor]
		public ConstructorWithIntegerParam(int a, int b)
		{
			A = a;
			B = b;
		}

		public int A { get; private set; }

		public int B { get; private set; }
	}
}
