using System;

namespace Compositional.Composer
{
	/// <summary>
	/// Specifies a composition point, by declaring required functionality, so that the Composer
	/// should fill the field or attribute with appropriate component instace to fulfill the request.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class ComponentPlugAttribute : Attribute
	{
		private readonly string _name;
		private readonly bool _required;

		public ComponentPlugAttribute()
			: this(null)
		{
		}

		public ComponentPlugAttribute(string name)
			: this(name, true)
		{
		}

		public ComponentPlugAttribute(bool required)
			: this(null, required)
		{
		}

		public ComponentPlugAttribute(string name, bool required)
		{
			_name = name;
			_required = required;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Required
		{
			get { return _required; }
		}
	}
}