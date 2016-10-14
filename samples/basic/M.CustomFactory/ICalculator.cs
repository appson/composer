namespace Compositional.Composer.Samples.Basic
{
	[Contract]
	public interface ICalculator
	{
		int Add(int a, int b);
		int Subtract(int a, int b);
		int Multiply(int a, int b);
		int Divide(int a, int b);
		int Remainder(int a, int b);
	}
}
