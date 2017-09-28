# Composer Quick Start Guide

## Scenario one: Simple component instantiation

#### Install NuGet packages

To start using Composer, start by installing the NuGet packages. Run this in Package Manager console:

```
Install-Package Appson.Composer.Implementation
```

or install the `Appson.Composer.Implementation` package using NuGet Package Manager UI.

#### Declare a contract type

Declare a `class` or `interface` to be a contract for a component. You use such contracts
to look up a component from Composer, that is, when you're looking for some component to *provide* the
services specified by the contract. Use `[Contract]` meta data attribute on a type to do so.

```csharp
    [Contract]
    public interface ILogger
    {
        void Log(string log);
    }
```

#### Write a component

Implement a concrete class that implements / extends the contract type

```csharp
    [Component]
    public class DefaultLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
```

#### Create an instance of Composer and register the component type

Composer is a simple class library, and you can simply instantiate the `ComponentContext` class
to have a new instance of Composer ready to use. Then use the `Register` method on the instance to
introduce component types to Composer.

```csharp
    var composer = new ComponentContext();
    composer.Register(typeof(DefaultLogger));
```

Composer will reflect the component type, and discover the *provided contracts*. After doing so, the
Composer **knows** someone who can *provide* the mentioned contract, so you can ask for it.

#### Query for the component

Ask Composer that "I want some object who can provide this service" using `GetComponent` method.

```csharp
    composer.GetComponent<ILogger>().Log("Hello, compositional world!");
```

Composer will find the appropriate component type (which is previously registered), instantiate it,
initialize it and return it.

Composer will also keep a reference to the component, so that it can respond to the later requests
with the same instance. So if you ask for the component again, the same instance is returned. (You can
change this behavior by specifying [component cache settings](api-ref/component-cache.md).)





## Scenario two: Dependency injection

When you ask Composer to prepare a component for you, it will also inject any declared dependencies
on the component to other components. Here's an example: you have three contracts declared as below:

```csharp
    [Contract]
    public interface IOrderLogic
    {
        void PlaceOrder(string customerName, int amount);
    }

    [Contract]
    public interface IOrderData
    {
        void SaveOrderData(string description);
    }

    [Contract]
    public interface ICustomerData
    {
        int GetCustomerId(string customerName);
    }
```

Each of the above contracts can have an implementation class (a class marked with `[Component]`).
But each of the components will need help from other components to achieve the goal. For placing
the order, `DefaultOrderLogic` will need to have a component that provides `ICustomerData` to
lookup the customer id, and it will also need someone with `IOrderData` capabilities to store
the new order. All of them will also need to log some data for later.

By placing `[ComponentPlug]` attribute on properties, each component can declare 
**required contracts**. It means that the component needs Composer to provide them with an
implementation of the contract before the component can function properly. Here's the code:

```csharp

    [Component]
    public class DefaultOrderLogic : IOrderLogic
    {
        [ComponentPlug] public IOrderData OrderData { get; set; }
        [ComponentPlug] public ICustomerData CustomerData { get; set; }
        [ComponentPlug] public ILogger Logger { get; set; }

        public void PlaceOrder(string customerName, int amount)
        {
            Logger.Log($"Placing order for {customerName} with the amount = {amount}");

            var customerId = CustomerData.GetCustomerId(customerName);
            OrderData.SaveOrderData($"Order for customer {customerId}: {amount} items");

            Logger.Log("Done.");
        }
    }

    [Component]
    public class DefaultOrderData : IOrderData
    {
        [ComponentPlug] public ILogger Logger { get; set; }

        public void SaveOrderData(string description)
        {
            Logger.Log($"Saving order: {description}");    
        }
    }

    [Component] public class DefaultCustomerData : ICustomerData
    {
        [ComponentPlug] public ILogger Logger { get; set; }

        public int GetCustomerId(string customerName)
        {
            Logger.Log($"Looking up customer with name {customerName}...");
            return 5;
        }
    }
```

When registering these components, Composer will identify these required contracts and build a graph.
Upon querying, when Composer instantiates the components, it will then **compose** them to each other
and form a completely initialized component before returning it.

After registering all components:

```csharp
    var composer = new ComponentContext();
    composer.Register(typeof(ConsoleLogger));
    composer.Register(typeof(DefaultCustomerData));
    composer.Register(typeof(DefaultOrderData));
    composer.Register(typeof(DefaultOrderLogic));
```

you can use the same `GetComponent` method to ask for an `IOrderLogic` component, and use it immediately,
without worrying about dependencies.

```csharp
    composer.GetComponent<IOrderLogic>().PlaceOrder("John", 17);
```