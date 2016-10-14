using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DefaultProgramRunner : IProgramRunner
	{
		#region Plugs

		private ICalculator _calculator;

		[ComponentPlug]
		public ICalculator Calculator
		{
			get
			{
				return _calculator;
			}
			set
			{
				Console.WriteLine("SET PLUG     - DefaultProgramRunner.Calculator({0})", value.GetType().Name);
				_calculator = value;
			}
		}

		#endregion

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
				Console.WriteLine("SET CONFIG   - DefaultProgramRunner.Verbose({0})", value);
				_verbose = value;
			}
		}

		#endregion

		#region Initialization

		public DefaultProgramRunner()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultProgramRunner");
		}

		[OnCompositionComplete]
		public void CompositionComplete()
		{
			if (Verbose)
				Console.WriteLine("NOTIFICATION - DefaultProgramRunner: OnCompositionComplete.");
		}

		#endregion

		#region Implementation of IProgramRunner

		public void Run()
		{
			Console.WriteLine();

			if (Verbose)
			{
				Console.WriteLine("METHOD CALL  - DefaultProgramRunner.Run()");
				Console.WriteLine();
			}

			Console.WriteLine("67 + 12 = {0}", Calculator.Add(67, 12));
			Console.WriteLine();

			Console.WriteLine("67 - 12 = {0}", Calculator.Subtract(67, 12));
			Console.WriteLine();
	
			Console.WriteLine("67 * 12 = {0}", Calculator.Multiply(67, 12));
			Console.WriteLine();

			Console.WriteLine("67 / 12 = {0} (with remainder = {1})", Calculator.Divide(67, 12), Calculator.Remainder(67, 12));
			Console.WriteLine();
		}

		#endregion
	}
}
