using System;

namespace Compositional.Composer
{
	/// <summary>
	/// Designates a field or property to be a Resource Manager Plug, in order for the
	/// component to recieve ResourceManager objects similar to the components.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public class ResourceManagerPlugAttribute : Attribute
	{
		private readonly string _id;
		private readonly bool _required;

		public ResourceManagerPlugAttribute(string id, bool required = true)
		{
			_id = id;
			_required = required;
		}

		public string Id
		{
			get { return _id; }
		}

		public bool Required
		{
			get { return _required; }
		}
	}
}