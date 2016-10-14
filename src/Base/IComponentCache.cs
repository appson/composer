namespace Appson.Composer
{
	public interface IComponentCache
	{
		ComponentCacheEntry GetFromCache(ContractIdentity contract);
		void PutInCache(ContractIdentity contract, ComponentCacheEntry entry);

		object SynchronizationObject { get; }
	}

	public class ComponentCacheEntry
	{
		public object ComponentInstance;
		public object OriginalComponentInstance;
	}
}