using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Appson.Composer.Resources
{
	public class ResourcePointerList : IResourcePointer
	{
		#region Private fields

		private List<IResourcePointer> _pointers;

		#endregion

		#region Public properties

		public List<IResourcePointer> Pointers
		{
			get { return _pointers; }
			set { _pointers = value; }
		}

		#endregion

		#region IResourcePointer implementation

		public void FillRepository(ResourceRepository repository)
		{
			if (_pointers == null)
				return;

			foreach (var pointer in _pointers)
				pointer.FillRepository(repository);
		}

		public ResourceManager GetResourceManager()
		{
			throw new InvalidOperationException("Calling GetResourceManager on a ResourcePointerList object is not permitted.");
		}

		public Stream GetStream()
		{
			throw new InvalidOperationException("Calling GetStream on a ResourcePointerList object is not permitted.");
		}

		#endregion
	}
}