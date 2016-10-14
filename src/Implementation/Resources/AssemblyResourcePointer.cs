using System;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Appson.Composer.Resources
{
	public class AssemblyResourcePointer : IResourcePointer
	{
		#region Private fields

		private string _id;
		private Assembly _assembly;
		private string _resourceName;

		#endregion

		#region Public properties

		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public Assembly Assembly
		{
			get { return _assembly; }
			set { _assembly = value; }
		}

		public string ResourceName
		{
			get { return _resourceName; }
			set { _resourceName = value; }
		}

		#endregion

		public void FillRepository(ResourceRepository repository)
		{
			if (repository == null)
				throw new ArgumentNullException("repository");

			if (string.IsNullOrEmpty(_id))
				throw new InvalidOperationException(
					"Id should be assigned to a non-empty string before filling the resource repository by an instance of AssemblyResourcePointer.");

			if (string.IsNullOrEmpty(_resourceName))
				throw new InvalidOperationException(
					"ResourceName should be assigned to a non-empty string before filling the resource repository by an instance of AssemblyResourcePointer.");

			if (_assembly == null)
				throw new InvalidOperationException(
					"Assembly should be assigned to a non-empty string before filling the resource repository by an instance of AssemblyResourcePointer.");

			repository.Register(_id, this);
		}

		public ResourceManager GetResourceManager()
		{
			if (_assembly == null)
				throw new InvalidOperationException("AssemblyResourcePointer instance is not initialized. Assembly cannot be null.");
			if (string.IsNullOrEmpty(_resourceName))
				throw new InvalidOperationException(
					"AssemblyResourcePointer instance is not initialized. ResourceName cannot be null or empty.");

			return new ResourceManager(_resourceName, _assembly);
		}

		public Stream GetStream()
		{
			return Assembly.GetManifestResourceStream(_resourceName);
		}
	}
}