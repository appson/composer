using System;

namespace Compositional.Composer
{
	/// <summary>
	/// Specifies a Configuration Point in a component, where a configuration
	/// value should be set by the composer in order for the component to work
	/// properly.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public class ConfigurationPointAttribute : Attribute
	{
		private readonly string _configurationVariableName;
		private readonly bool _required;

		public ConfigurationPointAttribute()
			: this(null, false)
		{
		}

		public ConfigurationPointAttribute(bool required)
			: this(null, required)
		{
		}

		public ConfigurationPointAttribute(string configurationVariableName, bool required = true)
		{
			_configurationVariableName = configurationVariableName;
			_required = required;
		}

		public string ConfigurationVariableName
		{
			get { return _configurationVariableName; }
		}

		public bool Required
		{
			get { return _required; }
		}
	}
}