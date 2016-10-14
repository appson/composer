using System;

namespace Compositional.Composer
{
	/// <summary>
	/// Specifies the constructor to be used to create the component instance when composing.
	/// Should be used only once per component (no more than one constructor in each class
	/// should have this attribute).
	/// </summary>
	[AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
	public sealed class CompositionConstructorAttribute : Attribute
	{
		private readonly string[] _names;

		public CompositionConstructorAttribute()
		{
		}

		public CompositionConstructorAttribute(params string[] names)
		{
			_names = names;
		}

		public string[] Names
		{
			get { return _names; }
		}
	}
}