# Composer API Reference - IComponentContext

Here's the definition of `IComponentContext` interface:

```csharp
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
```

Composer's implementation class, `ComponentContext`, implements `IComponentContext` and provides the above
functionality. This document summarizes the usage of each of the methods above.



## General behavior

These behaviors apply to all methods in this interface:

#### Thread-safety

None of the methods in this interface is thread-safe. You should practice caution when calling
these methods where there might be race conditions.

As a best practice, either initialize Composer and make all registrations on application startup 
and on the main thread, where you're sure there's no concurrency going on, or use `lock` or other
concurrency control structures to make sure no more than a single thread enter Composer methods
at a time while you're making registration changes.

#### IComponentContext is a Contract itself

The `IComponentContext` interface is itself marked with `[Contract]` attribute, and automatically registered
in the Composer upon creation of the `ComponentContext` class. So, you can reach an instance of the
`IComponentContext` similar to any other components in the composition (like as a constructor argument, using
`[ComponentPlug]` on a property or a field, ...)



## Registering components

Different overloads of `Register` method are used to introduce Composer to the new component types, and
make them available for the composition and injection.

TODO



## Unregistering components

TODO


## Setting or removing variables

TODO



## Managing composition listeners

TODO


