using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultAdder : IAdder
	{
		#region Configuration

		private bool _verbose;

		[ConfigurationPoint(false)]
		public bool Verbose
		{
			get
			{
				return _verbose;
			}
			set
			{
				Console.WriteLine("SET CONFIG   - DefaultAdder.Verbose");
				_verbose = value;
			}
		}

		#endregion

		#region Initialization

		public DefaultAdder()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultAdder");
		}

		#endregion

		#region Implementation of IAdder

		public int Add(int a, int b)
		{
			if (Verbose)
				Console.WriteLine("METHOD CALL  - DefaultAdder.Add({0}, {1})", a, b);

			return a + b;
		}

		#endregion
	}
}
