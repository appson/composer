using System;
using System.IO;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DriveListingPlugin : ICommandPlugin
	{
		#region Implementation of ICommandPlugin

		public string Command
		{
			get { return "drv"; }
		}

		public string Title
		{
			get { return "List the available system drives."; }
		}

		public void Execute()
		{
			var drives = DriveInfo.GetDrives();
			foreach (var driveInfo in drives)
			{
				Console.WriteLine(driveInfo);
			}
		}

		#endregion
	}
}
