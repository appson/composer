using System;
using System.Linq;
using System.Reflection;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class Program
	{
		static void Main()
		{
			var context = new ComponentContext();
			context.RegisterAssembly(Assembly.GetExecutingAssembly());

			var plugins = context.GetAllComponents<ICommandPlugin>();

			while(true)
			{
				Console.WriteLine("Type any of the commands to proceed:");
				Console.WriteLine("[exit] - Quit the application");
				foreach (var plugin in plugins)
				{
					Console.WriteLine("[{0}] - {1}", plugin.Command, plugin.Title);
				}

				Console.Write("> ");
				var command = (Console.ReadLine() ?? "").ToLower();

				if (command == "exit")
					break;

				var selectedPlugin = plugins.SingleOrDefault(p => p.Command.ToLower() == command);

				Console.WriteLine();
				Console.WriteLine("-----------------------------------------------------------");

				if (selectedPlugin != null)
					selectedPlugin.Execute();
				else
					Console.WriteLine("Error: Unknown command.");

				Console.WriteLine("-----------------------------------------------------------");
				Console.WriteLine();
			}
		}
	}
}
