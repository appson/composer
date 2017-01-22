using System.Web;
using Appson.Composer.Cache;

namespace Appson.Composer.Web.Cache
{
	[Contract]
	[Component]
	[ComponentCache(typeof (StaticComponentCache))]
	public class SessionComponentCache : IComponentCache
	{
		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			var contractString = GetContractString(contract);

			if (HttpContext.Current.Session[contractString] != null)
				return HttpContext.Current.Session[contractString] as ComponentCacheEntry;

			return null;
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
		{
			var contractString = GetContractString(contract);
			HttpContext.Current.Session[contractString] = entry;
		}

		public object SynchronizationObject => HttpContext.Current.Session;

	    #endregion

		#region Private helper methods

		private string GetContractString(ContractIdentity contract)
		{
			return "|ComponentCache|" + contract.Type.AssemblyQualifiedName +
			       "|" + (contract.Name ?? "<NULL>") + 
				   "|";
		}

		#endregion
	}
}