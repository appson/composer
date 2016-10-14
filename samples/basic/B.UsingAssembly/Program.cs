using System;
using System.Reflection;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
            // Create a new component context.

            var context = new ComponentContext();

            // Register the whole assembly.
            // This makes Composer to go through all of the types in
            // the assembly, searching for any public class that is
            // attributed with [Component], and registers it with
            // default settings.
            //
            // Assembly.GetExecutingAssembly() returns the current
            // assembly (the executable containing the Program class)

			context.RegisterAssembly(Assembly.GetExecutingAssembly());

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
