
# Composer API Reference - Component Caching

One of Composer's features is called *component caching*, which is how Composer can be instructed to
shares a component instance among multiple users. This is something that the author of a component
should have control over, depending on the nature of the component and its dependencies.

The component caching behavior is easily extensibe, and you can implement your own instance-sharing
logics and plug them into Composer. You can read about it in 
[custom component caching](../extension/cache.md) page. This page describes different component
caching logics embedded in the Composer library itself, and how to use them. 



## Default component caching behior

By default, when you register a component with Composer, the `DefaultComponentCache` will
be used. `DefaultComponentCache` behaves similar to *Singleton* pattern, making sure that the
component is not instantiated more than once, and causes all `GetComponent` queries to return
the same instance of the class.

Note: the default behavior makes each component behave as a singleton 
**per registration and per contract**. This means that:

* If you register a single component type multiple times, a separate instance will be created for each registration.

* If the component has multiple provided contracts (Eg. implements several interfaces that each of them is a contract), a separate instance will be returned when querying for each contract.



## Sharing instance among multiple contracts

The component cache type `ContractAgnosticComponentCache` can be used to share the instance of the component
among different contracts.



## Enforcing single instance in an AppDomain

Not only you may have different contracts, or register the component multiple times, you may also have created
different instances of Composer (multiple `ComponentContext` instances) in an application. If you want a component
to share the state among all of the registration among a single AppDomain, you can use `StaticComponentCache`
type to do so.

The `StaticComponentCache` logic caches the component instance in a static variable, per contract identity 
(contract type and contract name). So the instance is kept at `AppDomain` level.



## Setting a non-default component cache

You can specify the tpye of the component cache on component using `[ComponentCache]` attribute. For example:

```csharp
    [Component, ComponentCache(typeof(StaticComponentCache))]
    public class MyComponent : IMyContract
    {
        //...
    }
```

If you're using fluent syntax to register components, `UseComponentCache` method can be used to produce
the same results:

```csharp
composer.ForComponent<MyComponent>()
        .UseComponentCache<StaticComponentCache>()
        .RegisterWith<IMyContract>();
```



## Disabling component caching for a component

Many type instances cannot be re-used in any scenario, so you need to disable component caching for a specific
component completely. Examples are controllers in ASP.NET WebAPI or ASP.NET MVC applications, or controls in
a Windows Forms application. You may also use designs that require new objects on every use, which is perfectly
fine.

In such cases, you can simply pass `null` as the contract type for component cache, and it will disable the
caching for that component. 

```csharp
    [Component, ComponentCache(null)]
    public class MyComponent : IMyContract
    {
        //...
    }
```

```csharp
composer.ForComponent<MyComponent>()
        .UseComponentCache(null)
        .RegisterWith<IMyContract>();
```



## Beware of the concurrency issues

When you're using any component caching (including default behavior), your component instances may easily
be vulnerable to concurrency and race conditions, specially in heavily-multithreaded applications such
as web applications. Concurrency issues are naturally hidden from the eyes of the developer, hard to
detect, very hard to reproduce and debug, and basically can cause you to put your head through a wall.

So, when using any form of component caching (including default), you should either:

* Make sure your component is not used in multi threads at the same time, like if your application is not
multi-threaded or you only access Composer or components on the main thread

* Make sure your components **do not have any object-level or class-level state**. This means no fields,
no properties and no static states. Because they can be accessed from all threads entering methods of
your component

* Or closely guard concurrency on any access on your fields / properties on the component, using concurrency
control tools (such as `lock` or `Semaphone`). Take this option if you really know what you're doing.




## Different cache types on dependency chains

For components down the dependency chain, you cannot have *less* caching than the components up the chain.
This is because dependencies are only injected to the components when they are initialized. If new
instance of a component is not being initialized, it means that the dependencies are not queried and
injected again. So having less (or no) caching on the injected component has no effect.

For example, suppose we have `ComponentOne` which uses default caching, and `ComponentTwo` that we've
disabled the caching for. As long as we're querying `ComponentTwo` directly from Composer, everything
will be fine. But if `ComponentOne` has a dependency for `ComponentTwo`, and we query for `ComponentOne`,
Composer will create a new instance of `ComponentTwo` and inject it into `ComponentOne`, and caches the
newly-created instance of `ComponentOne`. From now on, all queries for `ComponentOne` return the same
cached instance, which already have an instance of `ComponentTwo` set. As a result, `ComponentTwo` is not
being re-created (and behaves as a cached component) although it has component caching disabled.

The reverse is okay, though, and quite typical. You can have *less* cached components depend on 
*more* cached components, and each time you ask for the less cached component, a new instance is
created, but the same instance of the dependency is injected into it.


