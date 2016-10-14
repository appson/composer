using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultMultiplier : IMultiplier
	{
		#region Initialization

		public DefaultMultiplier()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultMultiplier");
		}

		#endregion

		#region Implementation of IMultiplicator

		public int Multiply(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultMultiplier.Multiply({0}, {1})", a, b);

			return a * b;
		}

		#endregion
	}
}
