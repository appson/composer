using System;
using System.Linq.Expressions;

namespace Appson.Composer.FluentExtensions
{
    public class FluentLocalComponentConfig<TComponent> : FluentLocalComponentConfig
    {
        #region Constructors

        public FluentLocalComponentConfig(ComponentContext context) : base(context, typeof(TComponent))
        {
        }

        #endregion

        #region Fluent configuration methods

        public new FluentLocalComponentConfig<TComponent> SetComponent<TPlugContract>(
            string memberName, string contractName = null, bool required = true)
        {
            return SetComponent(memberName, typeof(TPlugContract), contractName, required);
        }

        public new FluentLocalComponentConfig<TComponent> SetComponent(
            string memberName, Type contractType, string contractName = null, bool required = true)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig<TComponent> SetComponent<TPlugContract>(
            Expression<Func<TComponent, TPlugContract>> member, string contractName = null, bool required = true)
        {
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> AddConstructorComponent<TPlugContract>(string contractName = null, bool required = true)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> AddConstructorComponent(Type contractType, string contractName = null, bool required = true)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> AddConstructorValue(object value)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> AddConstructorValue<TValue>(Func<IComposer, TValue> valueCalculator)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> AddConstructorValueFromVariable(string variableName)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> SetValue(string memberName, object value)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig<TComponent> SetValue<TMember>(Expression<Func<TComponent, TMember>> member, TMember value)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig<TComponent> SetValue<TMember>(Expression<Func<TComponent, TMember>> member,
            Func<IComposer, TMember> valueCalculator)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> SetValueFromVariable(string memberName, string variableName)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig<TComponent> SetValueFromVariable<TMember>(
            Expression<Func<TComponent, TMember>> member, string variableName)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> NotifyInitialized(string methodName)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig<TComponent> NotifyInitialized(Action<IComposer, TComponent> initAction)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> UseComponentCache(Type cacheContractType, string cacheContractName = null)
        {
            // TODO
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> UseComponentCache<TCacheContract>(string cacheContractName = null)
        {
            // TODO
            return this;
        }

        #endregion

    }
}