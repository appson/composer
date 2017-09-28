# Composer API Reference - IComposer

Here's the defintion of `IComposer` interface:

```csharp
	[Contract]
	public interface IComposer
	{
        ComposerConfiguration Configuration { get; }

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
```

Composer's implementation class, `ComponentContext`, implements `IComposer` and provides the above
functionality. This document summarizes the usage of each of the methods above.



## General behavior

These behaviors apply to all methods in this interface:

#### Attribute checking

Starting from version 1.4.1, query methods do not check `[Contract]` attribute on the queried
contract type, for the performance reason. All attribute checkings are performed during registration.

#### Thread-safety

All methods in IComposer interface are thread-safe and re-entrant among each-other, but not
with `IComponentContext` methods. This means that these methods should not be used during
registration or change in component configurations. 

But while no change is being made to the
component registrations, you're free to call the `IComposer` methods in any way. Caching, or
life-cycle management of component instances, are thread-safe and prevent creation of multiple
component instances (when cached) even if called concurrently.

#### IComposer is a Contract itself

The `IComposer` interface is itself marked with `[Contract]` attribute, and automatically registered
in the Composer upon creation of the `ComponentContext` class. So, you can reach an instance of the
`IComposer` similar to any other components in the composition (like as a constructor argument, using
`[ComponentPlug]` on a property or a field, ...)



## Configuration

The `Configuration` property encapsulates configuration settings that Composer uses to function.
For more information about it, see [Configuration](configuration.md) documentation.



## GetComponent overloads

Different `GetComponent` overloads are used to query Composer for a component. The type you pass to these
methods, is the contract type that the component provides.

Generic overloads (which has a `TContract` type parameter) can be used to avoid casting, and results in
more readable and cleaner code, unless you don't know the component type in compile time.

The overloads that do not have a `name` parameter, consider the component name to be `null`. To be
exact, they look for a `ContractIdentity` with the specified type and `null` name.

#### Not-found behavior

The query methods return `null` in case no component is registered with the composer that
provides the requested contract, with the requested name. It doesn't throw any exceptions.

## Multiple-registrations per contract identity

If multiple components are registered for a single contract identity (with the same contract type
and name), `GetComponent` method returns one of them. The returned one is not defined (might be
any one of the registered ones).



## GetAllComponents overloads

Different components can be registered with the same contract in the Composer. Even the same component
can be registered multiple times. While `GetComponent` returns one of them, `GetAllComponents` return
all of them.

Different `GetAllComponents` overloads are similar in logic to different `GetComponent` overloads, with
the difference that they return all of the registrations for a single contract identity. Thus, they all
return an `IEnumerable` of the contract type.

Note that they still operate for the same *contract identity*. If you register multiple components with
different names, but with the same contract type, you can't use `GetAllComponents` to retrieve them all.



## GetComponentFamily overloads

Regardless of the `name` used to register a component type, the `GetComponentFamily` method returns all
of the components that provide a specific contract type. It's like building contract identities with
different names (but same contract type), and then querying them using `GetAllComponents` and 
concatenating the result.



## GetVariable

Returns the contents of a Composer variable, given its name. Composer variables are means of storing 
arbitrary objects on the `ComponentContext` and pass them around your application, mostly used for 
configuration data.



## InitializePlugs overloads

You may have a case that you can't (or don't want to) take control for creation of an object, 
so you can't / don't want to make it a component and ask Composer to instantiate it, or manage 
its lifecycle. An example can be instantiation of `View` objects, where a framework does the
work.

But you may still need to use dependencies, and want them injected. In such a case, you can
declare `[ComponentPlugs]` and `[ConfigurationPoints]` as before, and ask Composer to fill them
on an already-instantiated object, using `InitializePlugs` method.

In this case, Composer will reflect on the object, identify dependencies and fills them up.
Composer will not cache or keep an instance to the object when initialized in this way, and you
(or the framework of your choice) is responsible to manage the object / component's lifecycle.
