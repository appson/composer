using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
			var context = new ComponentContext();
			context.ProcessCompositionXmlFromResource("Compositional.Composer.Samples.Basic.CalculatorComposition.xml");

			context.SetVariableValue("Verbose", true);

			context.GetComponent<IProgramRunner>().Run();
		}
	}
}
