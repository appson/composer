using System;
using System.Collections.Generic;
using System.Linq;

namespace Compositional.Composer
{
	internal class Repository
	{
		private readonly IDictionary<ContractIdentity, List<IComponentFactory>> _contracts;

		public Repository()
		{
			_contracts = new Dictionary<ContractIdentity, List<IComponentFactory>>();
		}

		public void Add(ContractIdentity identity, IComponentFactory factory)
		{
			if (!_contracts.ContainsKey(identity))
				_contracts.Add(identity, new List<IComponentFactory>());

			_contracts[identity].Add(factory);
		}

		public void Remove(ContractIdentity identity)
		{
			if (_contracts.ContainsKey(identity))
				_contracts.Remove(identity);
		}

		public void RemoveAll(Type type)
		{
			var identitiesToRemove = _contracts.Keys.Where(i => i.Type == type).ToArray();
			Array.ForEach(identitiesToRemove, i => _contracts.Remove(i));
		}

		public IEnumerable<IComponentFactory> FindFactories(ContractIdentity identity)
		{
			var closedResults = (_contracts.ContainsKey(identity)
			                     	? _contracts[identity]
			                     	: Enumerable.Empty<IComponentFactory>());

			if (identity.Type.IsGenericType)
			{
				var genericContractType = identity.Type.GetGenericTypeDefinition();
				var genericIdentity = new ContractIdentity(genericContractType, identity.Name);

				var genericResults = (_contracts.ContainsKey(genericIdentity)
				                      	? _contracts[genericIdentity]
				                      	: Enumerable.Empty<IComponentFactory>());

				return closedResults.Concat(genericResults);
			}

			return closedResults;
		}

		public IEnumerable<ContractIdentity> GetContractIdentityFamily(Type type)
		{
			return _contracts.Keys.Where(i => i.Type == type);
		}
	}
}