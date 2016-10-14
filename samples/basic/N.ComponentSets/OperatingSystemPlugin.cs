using System;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	class OperatingSystemPlugin : ICommandPlugin
	{
		#region Implementation of ICommandPlugin

		public string Command
		{
			get { return "os"; }
		}

		public string Title
		{
			get { return "Prints the current operating system name and version number"; }
		}

		public void Execute()
		{
			Console.WriteLine(Environment.OSVersion);
		}

		#endregion
	}
}
