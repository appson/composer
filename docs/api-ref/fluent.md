# Composer API Reference - Fluent Configuration

Composer provides a set of Fluent APIs to register and configure components in a readable way.

**Note:** If you're using Fluent APIs to register types not decorated with `[Component]` and `[Contract]` (or other) attributes for configuration, you should disable attribute checking. See [Composer Configuration](configuration.md) for more details.

## Simple component registration

The following code sample registers `ComponentType` class as a component, with the `IContractType` contract. It's the simplest form of using Fluent API.

```csharp
composer
    .ForComponent<ComponentType>()
    .RegisterWith<IContractType>();
```

You can also use `Register()` with no contract type specified, so that Composer looks for all provided contracts on the specified component:

```csharp
composer.ForComponent<ComponentType>().Register();
```

Between `ForComponent<T>()` method call and `RegisterWith<T>()` method call, you can add any number of configuration method calls (listed below) to more accurately describe the component initialization procedure.

You can also register an already-instantiated component (an `object`) in composer using the `ForObject(...)` extension method. It's equivalent of using `PreInitializedComponentFactory` to register a component. Here's the sample:

```csharp
composer.ForObject(o).RegisterWith<IContract>();
```

## Initialization features

There are various methods for you to specify how Composer should instantiate and initialize the component. Here's a relatively complete sample:

```csharp
composer
    .ForComponent<ComponentType>()
    .SetComponent(x => x.ComponentOne)
    .SetComponent(x => x.ComponentTwo, "name", required: false)
    .SetValue(x => x.SomeValue, "someString")
    .SetValue(x => x.SomeValue, cmpsr => "someValue")
    .SetValue(x => x.ComponentTwo, cmpsr => cmpsr.GetComponent<IComponentTwo>(), false)
    .SetValueFromVariable(x => x.SomeValue, "one")
    .UseConstructor(typeof(IComponentOne), typeof(IComponentTwo))
    .AddConstructorComponent<IComponentOne>("name", required: false)
    .AddConstructorValue(5)
    .AddConstructorValue(cmpsr => cmpsr.GetComponent<IComponentTwo>("name"))
    .AddConstructorValueFromVariable("two")
    .NotifyInitialized((cmpsr, x) => x.Initialize())
    .NotifyInitialized((cmpsr, x) => x.ParameterizedInit(5))
    .UseComponentCache<ContractAgnosticComponentCache>()
    .RegisterWith<IContractType>();
```

Here's what each method will do:

#### SetComponent
> can be used to specify that a member should be initialized with another component
> that should be queried from Composer. 
> The contract type is inferred from the lambda-expression 
> (can be specified as an argument in non-generic version), 
> and contract name can be specified. 
> The query is `required` by default, unless specified otherwise.

#### SetValue
> specifies that a property or field of the target component instance should be set
> to a specific value (not neccessarily a component). You can pass the value 
> directly, or provide a delegate of type `Func<IComposer, T>` to calculate
> the value in the moment of component creation.

#### SetValueFromVariable
> is same as `SetValue` (and the same effect can be achieved using that)
> but it's a more convenient way of setting a property
> or method with the contents of a variable from Composer.

#### UseConstructor
> receives a `Type[]` and can be used to specify which constructor should
> Composer use to instantiate the component. **Specifying the constructor is
> optional most of the times, even if you're using a non-default constructor.**
> But if you might have a `null` argument for the constructor, you will **have to**
> specify the constructor using this method, because in that case inferring the
> constructor type from the constructor arguments is not possible.

#### AddConstructorComponent, AddConstructorValue and AddConstructorValueFromVarialbe
> are all similar to corresponding `SetComponent` and `SetValue` alternatives, 
> but can be used to provide instantiation-time arguments.

#### NotifyInitialized
> can be used to add arbitrary logic to component initialization. You can call this
> method as many times and build up a list of initialization logic. Each time, you pass
> a delegate that will be invoked upon creating the component instance.

#### UseComponentCache
> specifies which component cache type to be used for the component. By default,
> the component instance will be cached for each registration, and returned in all
> subsequent inquiries. If you want to disable caching for this component (also called *Transient* or *Prototype*) call this method with `null` argument.