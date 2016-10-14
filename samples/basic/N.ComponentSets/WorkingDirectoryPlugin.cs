using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	class WorkingDirectoryPlugin : ICommandPlugin
	{
		#region Implementation of ICommandPlugin

		public string Command
		{
			get { return "pwd"; }
		}

		public string Title
		{
			get { return "Prints the current working directory on the console"; }
		}

		public void Execute()
		{
			Console.WriteLine(Environment.CurrentDirectory);
		}

		#endregion
	}
}
