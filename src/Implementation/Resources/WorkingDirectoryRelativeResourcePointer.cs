using System;
using System.IO;
using System.Resources;

namespace Compositional.Composer.Resources
{
	public class WorkingDirectoryRelativeResourcePointer : IResourcePointer
	{
		#region IResourcePointer implementation

		public void FillRepository(ResourceRepository repository)
		{
			throw new NotImplementedException();
		}

		public ResourceManager GetResourceManager()
		{
			throw new NotImplementedException();
		}

		public Stream GetStream()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}