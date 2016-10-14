using Compositional.Composer.Emitter;
using Compositional.Composer.Factories;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
			var context = new ComponentContext();
			context.ProcessCompositionXmlFromResource("Compositional.Composer.Samples.Basic.CalculatorComposition.xml");

			var classEmitter = context.GetComponent<IClassEmitter>();
			IEmittedTypeHandler emittedTypeHandler = new TestEmittedTypeHandler();

			context.Register(new PreInitializedComponentFactory(classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof(IAdder))));
			context.Register(new PreInitializedComponentFactory(classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof(IMultiplier))));
			context.Register(new PreInitializedComponentFactory(classEmitter.EmitInterfaceInstance(emittedTypeHandler, typeof(IDivider))));

			context.GetComponent<IProgramRunner>().Run();
		}
	}
}
