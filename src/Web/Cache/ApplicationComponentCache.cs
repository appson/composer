using System.Web;
using Appson.Composer.Cache;

namespace Appson.Composer.Web.Cache
{
	[Contract]
	[Component]
	[ComponentCache(typeof(StaticComponentCache))]
	public class ApplicationComponentCache : IComponentCache
	{
		private const string AppKeyPrefix = "Appson.Composer.Web.Cache.Application.";

		#region Implementation of IComponentCache

		public ComponentCacheEntry GetFromCache(ContractIdentity contract)
		{
			return HttpContext.Current.Application.Get(GetIdentityString(contract)) as ComponentCacheEntry;
		}

		public void PutInCache(ContractIdentity contract, ComponentCacheEntry entry)
		{
			HttpContext.Current.Application.Set(GetIdentityString(contract), entry);
		}

		public object SynchronizationObject => HttpContext.Current.Application;

	    #endregion

		#region Private helper methods

		private static string GetIdentityString(ContractIdentity contract)
		{
			if (contract == null)
				return AppKeyPrefix + "<NullContract>";

			if (contract.Type == null)
				return AppKeyPrefix + "<NullType>";

			if (contract.Name == null)
				return AppKeyPrefix + contract.Type.FullName;

			return AppKeyPrefix + contract.Type.FullName + "," + contract.Name;
		}

		#endregion
	}
}