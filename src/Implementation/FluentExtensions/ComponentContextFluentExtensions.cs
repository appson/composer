using System;

namespace Appson.Composer.FluentExtensions
{
    public static class ComponentContextFluentExtensions
    {
        public static FluentLocalComponentConfig<T> ForComponent<T>(this ComponentContext context)
        {
            return new FluentLocalComponentConfig<T>(context);
        }

        public static FluentLocalComponentConfig ForComponent(this ComponentContext context, Type componentType)
        {
            return new FluentLocalComponentConfig(context, componentType);
        }

        public static FluentPreInitializedComponentConfig ForObject(this ComponentContext context, object instance)
        {
            var result = new FluentPreInitializedComponentConfig(context);
            result.Instance = instance ?? throw new ArgumentNullException(nameof(context));

            return result;
        }
    }
}