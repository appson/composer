using System.Collections.Generic;

namespace Appson.Composer.Cache
{
	[Contract]
	[Component]
	[ComponentCache(null)]
	public class StaticComponentCache : IComponentCache
	{
		private static readonly IDictionary<ContractIdentity, ComponentCacheEntry> cacheContent =
			new Dictionary<ContractIdentity, ComponentCacheEntry>();

		private static readonly object synchronizationObject = new object();

		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
		    return cacheContent.TryGetValue(contract, out var entry) ? entry : null;
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
		{
			cacheContent[contract] = entry;
		}

		public object SynchronizationObject => synchronizationObject;

	    #endregion
	}
}