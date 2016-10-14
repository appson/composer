using System;

namespace Appson.Composer
{
	/// <summary>
	/// Marks a class as being a "Component", which can then provide functionality to
	/// other components based on provided contracts, and ask for functionality from
	/// other components by having "Plugs", based on required contracts.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ComponentAttribute : Attribute
	{
		private readonly string _defaultName;

		public ComponentAttribute()
			: this(null)
		{
		}

		public ComponentAttribute(string defaultName)
		{
			_defaultName = defaultName;
		}

		public string DefaultName
		{
			get { return _defaultName; }
		}
	}
}