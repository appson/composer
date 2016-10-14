namespace Appson.Composer.Cache
{
	[Contract]
	[Component]
	[ComponentCache(null)]
	public class ContractAgnosticComponentCache : IComponentCache
	{
		private ComponentCacheEntry _cacheContent = null;

		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			return _cacheContent;
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
		{
			_cacheContent = entry;
		}

		public object SynchronizationObject
		{
			get { return this; }
		}

		#endregion
	}
}