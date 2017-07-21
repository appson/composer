using System;

namespace Appson.Composer
{
	[Contract]
	public interface IComponentContext : IComposer
	{
		void Register(Type contract, Type component);
		void Register(Type component);
		void Register(Type contract, string name, Type component);
		void Register(IComponentFactory componentFactory);
		void Register(string name, IComponentFactory componentFactory);
		void Register(string name, Type componentType);
		void Register(Type contract, IComponentFactory factory);
		void Register(Type contract, string name, IComponentFactory factory);

		void Unregister(ContractIdentity identity);
		void UnregisterFamily(Type type);

		void SetVariableValue(string name, object value);
		void SetVariable(string name, Lazy<object> value);
		void RemoveVariable(string name);

		void RegisterCompositionListener(string name, ICompositionListener listener);
		void UnregisterCompositionListener(string name);
	}
}
