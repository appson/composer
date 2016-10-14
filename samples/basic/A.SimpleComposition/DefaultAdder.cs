using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultAdder : IAdder
	{
		#region Initialization

		public DefaultAdder()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultAdder");
		}

		#endregion

		#region Implementation of IAdder

		public int Add(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultAdder.Add({0}, {1})", a, b);

			return a + b;
		}

		#endregion
	}
}
