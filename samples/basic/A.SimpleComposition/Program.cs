using System;

namespace Compositional.Composer.Samples.Basic
{
	public class Program
	{
		static void Main()
		{
			// Create a new component context.

            var context = new ComponentContext();

			// Register the types into the context individually.

            context.Register(typeof(DefaultCalculator));

			// The following component registrations is not referenced
            // directly from the main method, but is injected into the
            // plugs of DefaultCalculator.

            context.Register(typeof(DefaultAdder));
			context.Register(typeof(DefaultMultiplier));
			context.Register(typeof(DefaultDivider));

			// When calling GetComponent, Composer will instantiate
            // the components and fill the plugs recursively until
            // the component graph is completely initialized.
            
            var calculator = context.GetComponent<ICalculator>();
			Console.WriteLine();

			// Calling methods of ICalculator, which is realized by
            // DefaultCalculator class.
            
            Console.WriteLine("67 + 12 = {0}", calculator.Add(67, 12));
			Console.WriteLine();

			Console.WriteLine("67 - 12 = {0}", calculator.Subtract(67, 12));
			Console.WriteLine();
			
			Console.WriteLine("67 * 12 = {0}", calculator.Multiply(67, 12));
			Console.WriteLine();
			
			Console.WriteLine("67 / 12 = {0} (with remainder = {1})", calculator.Divide(67, 12), calculator.Remainder(67, 12));
			Console.WriteLine();
		}
	}
}
