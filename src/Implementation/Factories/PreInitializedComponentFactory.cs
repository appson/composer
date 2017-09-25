using System;
using System.Collections.Generic;

namespace Appson.Composer.Factories
{
	public class PreInitializedComponentFactory : IComponentFactory
	{
		private readonly object _componentInstance;

		#region Constructors

		public PreInitializedComponentFactory(object componentInstance)
		{
		    _componentInstance = componentInstance ?? throw new ArgumentNullException(nameof(componentInstance));
		}

		#endregion

		#region IComponentFactory Members

		public void Initialize(IComposer composer)
		{
		}

		public object Clone()
		{
			return CloneComponentFactory();
		}

		public IComponentFactory CloneComponentFactory()
		{
			return new PreInitializedComponentFactory(_componentInstance);
		}

		public IEnumerable<Type> GetContractTypes()
		{
			return ComponentContextUtils.FindContracts(_componentInstance.GetType());
		}

		public bool SharedAmongContracts => true;

	    public object GetComponentInstance(ContractIdentity contract,
		                                   IEnumerable<ICompositionListener> listenerChain)
		{
			return _componentInstance;
		}

		#endregion
	}
}