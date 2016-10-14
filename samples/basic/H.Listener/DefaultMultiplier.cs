using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultMultiplier : IMultiplier
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
				Console.WriteLine("SET CONFIG   - DefaultMultiplier.Verbose({0})", value);
				_verbose = value;
			}
		}

		#endregion

		#region Initialization

		public DefaultMultiplier()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultMultiplier");
		}

		[OnCompositionComplete]
		public void CompositionComplete()
		{
			if (Verbose)
				Console.WriteLine("NOTIFICATION - DefaultMultiplier: OnCompositionComplete.");
		}

		#endregion

		#region Implementation of IMultiplicator

		public int Multiply(int a, int b)
		{
			if (Verbose)
				Console.WriteLine("METHOD CALL  - DefaultMultiplier.Multiply({0}, {1})", a, b);

			return a * b;
		}

		#endregion
	}
}
