using System;

namespace Appson.Composer
{
	/// <summary>
	/// Marks method(s) to be run after the composition of the containing component is performed
	/// </summary>
	/// <remarks>
	/// In a Component class, all methods marked with this attribute are run automatically 
	/// by Composer once the composition of the component is completed. (after setting all
	/// of the plugs and configuration points)
	/// </remarks>
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class OnCompositionCompleteAttribute : Attribute
	{
	}
}