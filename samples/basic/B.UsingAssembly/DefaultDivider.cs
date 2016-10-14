using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultDivider : IDivider
	{
		#region Initialization

		public DefaultDivider()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultDivider");
		}

		#endregion

		#region Implementation of IDivider

		public int Divide(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultDivider.Divide({0}, {1})", a, b);

			return a/b;
		}

		public int Remainder(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultDivider.Remainder({0}, {1})", a, b);

			return a % b;
		}

		#endregion
	}
}
