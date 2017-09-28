
# Composer API Reference - Configuration

Composer has configuration options that let you tune how it behaves. These configuration options
are all wrapped inside `ComponentContext.Configuration` property, which is of `ComposerConfiguration` type.

In the current version of Composer, there's only a single option in the settings.


## Disabling attribute checking

By default, composer checks for attributes (such as `[Component]` and `[Contract]`) on types, and
throws exceptions when it doesn't find appropriate attributes. This is similar to type checking in
a statically-typed programming language, which is to make sure the configuration isn't wrong before
actually using it.

Although not recommended, if you find this undesirable in your scenario, you can turn of this 
validation using the following setting:

```csharp
composer.Configuration.DisableAttributeChecking = true;
```

This doesn't affect the *searching* for attributes, but only validation. For example, when disabled,
you can still use `Register` overloads that automatically search for contracts based on attributes, or
you can ask Composer to search an assembly and register all types marked with `[Component]`. The only
change is that Composer won't complain if you try to register a type that is not marked with attributes.

The setting can be changed any number of times for a single `ComponentContext` instance, and takes
effect immediately for registrations from that moment. Previously registered components are not affected.
So, you can use patterns such as disabling attribute checking, registering the unmarked components, and
then re-enabling it again.
