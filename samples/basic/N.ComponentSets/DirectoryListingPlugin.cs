using System;
using System.IO;

namespace Compositional.Composer.Samples.Basic
{
	[Component]
	public class DirectoryListingPlugin : ICommandPlugin
	{
		#region Implementation of ICommandPlugin

		public string Command
		{
			get { return "dir"; }
		}

		public string Title
		{
			get { return "List the files and folders in the C: drive"; }
		}

		public void Execute()
		{
			var folderNames = Directory.EnumerateDirectories("C:\\");
			foreach (var folderName in folderNames)
			{
				Console.WriteLine("<dir>  {0}", folderName);
			}

			var fileNames = Directory.EnumerateFiles("C:\\");
			foreach (var fileName in fileNames)
			{
				Console.WriteLine("<file>  {0}", fileName);
			}
		}

		#endregion
	}
}
