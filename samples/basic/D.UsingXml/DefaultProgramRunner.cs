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
				Console.WriteLine("SET PLUG     - DefaultProgramRunner.Calculator");
				_calculator = value;
			}
		}

		#endregion

		#region Initialization

		public DefaultProgramRunner()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultProgramRunner");
		}

		#endregion

		#region Implementation of IProgramRunner

		public void Run()
		{
			Console.WriteLine();

			Console.WriteLine("METHOD CALL  - DefaultProgramRunner.Run()");
			Console.WriteLine();

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
