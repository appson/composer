using System.Collections.Generic;

namespace Appson.Composer.Cache
{
	[Contract]
	[Component]
	[ComponentCache(null)]
	public class DefaultComponentCache : IComponentCache
	{
		private readonly IDictionary<ContractIdentity, ComponentCacheEntry> _cacheContent =
			new Dictionary<ContractIdentity, ComponentCacheEntry>();

		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			return _cacheContent.ContainsKey(contract) ? _cacheContent[contract] : null;
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
		{
			_cacheContent[contract] = entry;
		}

		public object SynchronizationObject
		{
			get { return this; }
		}

		#endregion
	}
}