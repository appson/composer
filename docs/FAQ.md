# Frequently Asked Questions

## Supported runtimes and dependencies

#### Q: Does Composer have any dependencies?
> No. It's built on .NET Framework version 4.0 with absolutely no external dependencies.

#### Q: Does Composer support .NET Core?
> Currently there are no builds for .NET Core on NuGet, simply because it's not yet needed anywhere.
> But since there are no dependencies, they can easily be built. If you need to use Composer in a .NET Core environment,
> drop us a line and we'll do the work.

## Design

#### Compositional Architecture? What?!
> Composer is meant to be more than just IoC/DI. Although very similar in features, it's rather built to promote
> and facilitate designing software that is formed from well-defined, replaceable or composable parts. What happens in
> action is very similar to IoC/DI containers with Auto Wiring turned on, and there's little difference visible (if any).

#### What is IoC/DI?
> It's a relatively-popular design pattern used in Object Oriented programming. 
> The basic ideas behind the pattern is best described by Martin Fowler here: https://martinfowler.com/articles/injection.html

## Features

#### Which injection style does Composer support? Property-injection or Constructor-injection?
> Both.
>
> It's recommended to use property-injection, but Composer can also identify constructor arguments and inject
> them as well, when creating a component instance. You can even use them together at the same time, having both
> constructor arguments and injection properties on a single component at the same time.

#### Does Composer support cyclic dependencies
> Yes. Also it might be a sign that you have a flaw in your design, Composer can handle cyclic dependencies between
> components and will not throw `StackOverflowException` or anything when there's a loop instantiating the components.
> Although this is only true when you're using property injection. For constructor injection, there's no way of
> instantiating an object with arguments that will be created later!

