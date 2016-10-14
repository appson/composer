using System;

namespace Appson.Composer
{
	/// <summary>
	/// Specifies the type can be a contract for composing several components.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	public sealed class ContractAttribute : Attribute
	{
	}
}