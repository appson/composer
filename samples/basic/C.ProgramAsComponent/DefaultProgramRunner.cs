using System;

namespace Compositional.Composer.Samples.Basic
{
	/// <summary>
	/// Component that contains the main program code. This
    /// code is called when the program starts running.
    /// 
    /// Placing the main program code allows the developer
    /// to access all of the components in compositional
    /// manner, plugged (injected) into the component's
    /// properties.
	/// </summary>
    [Component]
	public class DefaultProgramRunner : IProgramRunner
	{
		#region Plugs

		// Since we are coding inside a component which is
        // instantiated by Composer, we can declare plugs for
        // the dependencies, instead of looking them up from
        // the component context in the code.

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

		/// <summary>
		/// Called when the program starts.
		/// </summary>
        public void Run()
		{
            // The main program code is moved here.

            // When the execution gets to this point, all
            // of the required components are created and
            // injected in their appropriate place in the
            // component graph.

			Console.WriteLine();

			Console.WriteLine("METHOD CALL  - DefaultProgramRunner.Run()");
			Console.WriteLine();

            // Calling methods of ICalculator, which is realized by
            // DefaultCalculator class.
            
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
