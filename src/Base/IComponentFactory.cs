using System;
using System.Collections.Generic;

namespace Appson.Composer
{
	/// <summary>
	/// Specifies the interface required by factories that actually create the component instance.
	/// </summary>
	public interface IComponentFactory
	{
		void Initialize(IComposer composer);
		IEnumerable<Type> GetContractTypes();

		object GetComponentInstance(ContractIdentity contract, IEnumerable<ICompositionListener> listenerChain);
	}
}