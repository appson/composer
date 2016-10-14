using System;

namespace Appson.Composer
{
	/// <summary>
	/// Instructs the registration procedure to ignore the marked component
	/// when registering all of the components in an assembly in batch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class IgnoredOnAssemblyRegistrationAttribute : Attribute
	{
	}
}