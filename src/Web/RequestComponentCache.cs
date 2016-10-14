using System.Web;

namespace Appson.Composer.Web
{
	[Contract]
	[Component]
	public class RequestComponentCache : IComponentCache
	{
		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			HttpContext.Current.Request.GetType();
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