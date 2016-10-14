using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
			var context = new ComponentContext();
			context.ProcessCompositionXmlFromResource("Compositional.Composer.Samples.Basic.CalculatorComposition.xml");

			context.Register(new CalculatorFactory());

			context.GetComponent<IProgramRunner>().Run();
		}
	}
}
