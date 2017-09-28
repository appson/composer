
# Composer Sample - Quick Start scenario two

In this sample, the first quick start sample is extended to show how
components can depend upon each other, and how Composer detects and injects 
dependencies among them.

[Complete source of this sample](../../samples/quickstart-2)



When you ask Composer to prepare a component for you, it will also inject any declared dependencies
on the component to other components. Here's an example, where you have three contracts declared as below:

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