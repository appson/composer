using System;
using System.Collections.Generic;

namespace Appson.Composer
{
//	public delegate void CompositionCompletionNotificationDelegate();

	/// <summary>
	/// Specifies the interface using which the code can query the Composer and
	/// ask for component instances.
	/// </summary>
	[Contract]
	public interface IComposer
	{
		// Component Lookup methods

		TContract GetComponent<TContract>() where TContract : class;
		TContract GetComponent<TContract>(string name) where TContract : class;
		object GetComponent(Type contract);
		object GetComponent(Type contract, string name);

		IEnumerable<TContract> GetAllComponents<TContract>() where TContract : class;
		IEnumerable<TContract> GetAllComponents<TContract>(string name) where TContract : class;
		IEnumerable<object> GetAllComponents(Type contract);
		IEnumerable<object> GetAllComponents(Type contract, string name);

		IEnumerable<TContract> GetComponentFamily<TContract>();
		IEnumerable<object> GetComponentFamily(Type contract);

		// Variable Lookup methods

		object GetVariable(string name);

		// Other methods

		void InitializePlugs<T>(T componentInstance);
		void InitializePlugs(object componentInstance, Type componentType);
	}
}