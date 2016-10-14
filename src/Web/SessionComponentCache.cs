namespace Compositional.Composer.Web
{
	[Contract]
	[Component]
	public class SessionComponentCache : IComponentCache
	{
		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			throw new System.NotImplementedException();
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry component)
		{
			throw new System.NotImplementedException();
		}

		public object SynchronizationObject
		{
			get { throw new System.NotImplementedException(); }
		}

		#endregion
	}
}