namespace Compositional.Composer.Samples.Basic
{
	[Contract]
	public interface IDivider
	{
		int Divide(int a, int b);
		int Remainder(int a, int b);
	}
}
