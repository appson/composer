using System.Collections.Generic;
using System.Threading;

namespace Appson.Composer.Cache
{
    [Contract]
    [Component]
    [ComponentCache(typeof(StaticComponentCache))]
    public class ThreadLocalComponentCache : IComponentCache
    {
        private readonly ThreadLocal<IDictionary<ContractIdentity, ComponentCacheEntry>> _cacheContent =
            new ThreadLocal<IDictionary<ContractIdentity, ComponentCacheEntry>>(() =>
                new Dictionary<ContractIdentity, ComponentCacheEntry>());

        private static readonly object synchronizationObject = new object();

        #region Implementation of IComponentCache

        public ComponentCacheEntry GetFromCache(ContractIdentity contract)
        {
            return _cacheContent.Value.TryGetValue(contract, out var entry) ? entry : null;
        }

        public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
        {
            _cacheContent.Value[contract] = entry;
        }

        public object SynchronizationObject => synchronizationObject;

        #endregion
    }
}