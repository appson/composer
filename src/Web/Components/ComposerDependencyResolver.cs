using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using Appson.Composer.Web.Contracts;

namespace Appson.Composer.Web.Components
{
	[Component]
	public class ComposerDependencyResolver : IDependencyResolverContract
	{
		private readonly IDependencyResolver _baseResolver;

		[ComponentPlug]
		public IComposer Composer { get; set; }

		public ComposerDependencyResolver()
		{
			_baseResolver = DependencyResolver.Current;
		}

		#region Implementation of IDependencyResolver

		public object GetService(Type serviceType)
		{
			object result = null;

			if (ComponentContextUtils.HasContractAttribute(serviceType))
				result = Composer.GetComponent(serviceType);

			if (result == null)
			{
				result = _baseResolver.GetService(serviceType);

				if (result != null)
					Composer.InitializePlugs(result, result.GetType());
			}

			return result;
		}

		[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
		public IEnumerable<object> GetServices(Type serviceType)
		{
			var baseResult = _baseResolver.GetServices(serviceType);

			baseResult.ToList().ForEach(o => Composer.InitializePlugs(o, o.GetType()));
			return ComponentContextUtils.HasContractAttribute(serviceType) ? Composer.GetAllComponents(serviceType).Concat(baseResult) : baseResult;
		}

		#endregion
	}
}