using System;
using System.Collections.Generic;
using System.Linq;

namespace Appson.Composer
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
		    _contracts.TryGetValue(identity, out var closedResults);
		    if (!identity.Type.IsGenericType)
                return closedResults ?? Enumerable.Empty<IComponentFactory>();

		    var genericContractType = identity.Type.GetGenericTypeDefinition();
		    var genericIdentity = new ContractIdentity(genericContractType, identity.Name);

		    if (_contracts.TryGetValue(genericIdentity, out var genericResults))
		    {
		        return closedResults?.Concat(genericResults) ?? genericResults;
		    }

		    return closedResults ?? Enumerable.Empty<IComponentFactory>();
		}

		public IEnumerable<ContractIdentity> GetContractIdentityFamily(Type type)
		{
			return _contracts.Keys.Where(i => i.Type == type);
		}
	}
}