using System;
using Compositional.Composer.Emitter;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
			var context = new ComponentContext();
			var classEmitter = context.GetComponent<IClassEmitter>();
			
			IEmittedTypeHandler emittedTypeHandler = new TestEmittedTypeHandler();

			var adder = (IAdder) classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof (IAdder));
			var multiplier = (IMultiplier) classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof (IMultiplier));
			var divider = (IDivider)classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof(IDivider));

			Console.WriteLine("67 + 12 = {0}", adder.Add(67, 12));
			Console.WriteLine();

			Console.WriteLine("67 * 12 = {0}", multiplier.Multiply(67, 12));
			Console.WriteLine();
			
			Console.WriteLine("67 / 12 = {0}", divider.Divide(67, 12));
			Console.WriteLine();
			
			Console.WriteLine("67 % 12 = {0}", divider.Remainder(67, 12));
			Console.WriteLine();
		}
	}
}
