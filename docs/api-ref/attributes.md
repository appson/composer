# Composer API Reference - Attributes

Composer defined some meta data attributes (derived from .Net's `Attribute` class) that can be used
to describe expected behavior from Composer when instantiating and composing objects together.
They are *descriptive* way of telling Composer what to do when initializing components.

**Note:** Composer also provides a *prescriptive* way, using a Fluent API. See
[Fluent API](fluent.md) for more information.



### Why attributes are required?
Up until version 1.3 of Composer, using these attributes (at least `[Component]` and 
`[Contract]`) was mandatory. Starting from version 1.4, there's a configuration option to disable
attribute checking altogether, in case you don't want to / cannot use Composer's attribute on your code.
See [Configuration](configuration.md) for how to disable attribute checking.

Anyway, author's opinion is strongly toward *NOT* disabling attribute checks, unless you need to work
with external code and libraries that you don't have control over.

Reason? When you're designing your system, it's important to think in **compositional** way. For that,
you explicitly define requirements for components, and they are fundamentally different from a typical
object-oriented abstraction (say `interface`s). But we're using an OO language to describe that after all,
so we're bound to use constructs from the language. Marking types to denote what you meant, is the least
you can to to boost your code's *compositional* readability.

For example, the `IEnumerable` interface is never meant to be a contract for a component, nor does `IDisposable`.
These are interfaces in the eye of the OO language, and they are the same as, let's say, `ICustomerStore` which
might be meant for a component responsible for storing customer data.

By decorating the interface with `[Contract]` attribute, you'll let the reader (or your-future-self) know that
the interface is meant to play as a key for composing components, not a mere OO abstraction. Naming conventions
can help too, but nothing is more readable than an explicit declaration.



## List of attributes

#### [Component]
> As expected, specifies the type is a component that provides some contracts. When scanning the assembly
> for components, Composer looks for types decorated with this attribute. Should only be placed on concrete
> classes with public constructors, accessible to Composer to instantiate, and have at least one provided
> contract (either the class itself, or any type in inheritance hierarchy).

#### [Contract]
> Specifies that the type can be used as a key for composing components to each other. A contract can be
> used as a **provided contract** when it is implemented / extended by a component, or be used as a
> **required contract** when a component declares a `[ComponentPlug]` on a property, or receives the
> type as a constructor argument.

#### [ComponentPlug]
> Can be placed on a public field or property of a component, and specifies a *required contract*. Composer
> will try and fill the field/property when instantiating the component. The property / field type should be
> a contract, and it will be used to query for a component. If no component is found to provide the contract,
> Composer will throw an exception unless the `required` parameter is set to false.

#### [ConfigurationPoint]
> Can be placed on a public field or property of a component, and specifies that a configuration value should
> be set on the member when Composer initializes the component. The configuration value can be specified when
> registering a component in code, in XML, using Fluent API, or via Composer's variables.

#### [ComponentMultiPlug]
> Similar to `[ComponentPlug]`, but instructs Composer to initialize the point with all of the components
> registered with the contract. Can be placed on `IList`, `IEnumerable`, `ICollection`, or array property /
> fields.

#### [ResourceManagerPlug]
> Designates a field or property on a component to be filled with a `ResourceManager` object similar
> to the way components are injected.

#### [ComponentCache]
> Can be placed on a component (along with `[Component]`attributed ) to specify which implementation 
> of `IComponentCache` should be used for component life-cycle management for the given component. 
> Should point to a component that is registered separately in the same `ComponentContext`. For more
> information, see (Comonent Caching)[component-cache.md].

#### [CompositionConstructor]
> Can be used on a constructor in a component class, to specify which constructor to be used when
> instantiating the component class by Composer. Only required when the component has multiple
> candidate constructors (public constructors), but may also be used to increase readability.

#### [IgnoredOnAssemblyRegistration]
> Instructs Composer to skip the given component when scanning and registering all components in
> an assembly. Can only be placed on components (along with `[Component]` attribute).

#### [OnCompositionComplete]
> Instructs Composer to invoke a method on the component, for further initialization, after the component
> is created and all of the Composer's initializations are complete (plugs, configuration points, etc.).
> Can only be placed on public method of a component that do not receive any parameters, and return `null`.
> The same effect can also be achieved by implementing `INotifyCompositionCompletion` interface by the component. 

