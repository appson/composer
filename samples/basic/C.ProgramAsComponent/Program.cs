using System.Reflection;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
            // Create a new component context, and register all
            // available components in the assembly at once.
            
            var context = new ComponentContext();
			context.RegisterAssembly(Assembly.GetExecutingAssembly());

			// Lookup the main component from Composer, which
            // will handle the main logic of the application.
            
            // When the "GetComponent" method below is called,
            // the whole application graph is instantiated
            // recursively, and all of the dependencies are
            // set into the component plugs, before the
            // GetComponent method returns. So, when we call
            // the Run() method, everything is already prepared
            // for the application to run.

            context.GetComponent<IProgramRunner>().Run();
		}
	}
}
