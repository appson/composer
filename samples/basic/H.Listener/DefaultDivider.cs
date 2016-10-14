using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultDivider : IDivider
	{
		#region Configuration

		private bool _verbose;

		[ConfigurationPoint("Verbose", false)]
		public bool Verbose
		{
			get
			{
				return _verbose;
			}
			set
			{
				Console.WriteLine("SET CONFIG   - DefaultDivider.Verbose({0})", value);
				_verbose = value;
			}
		}

		#endregion

		#region Initialization

		public DefaultDivider()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultDivider");
		}

		[OnCompositionComplete]
		public void CompositionComplete()
		{
			if (Verbose)
				Console.WriteLine("NOTIFICATION - DefaultDivider: OnCompositionComplete.");
		}

		#endregion

		#region Implementation of IDivider

		public int Divide(int a, int b)
		{
			if (Verbose)
				Console.WriteLine("METHOD CALL  - DefaultDivider.Divide({0}, {1})", a, b);

			return a/b;
		}

		public int Remainder(int a, int b)
		{
			if (Verbose)
				Console.WriteLine("METHOD CALL  - DefaultDivider.Remainder({0}, {1})", a, b);

			return a % b;
		}

		#endregion
	}
}
