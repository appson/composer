using System;
using System.Reflection;
using Appson.Composer.CompositionalQueries;
using Appson.Composer.Factories;

namespace Appson.Composer.FluentExtensions
{
    public class FluentLocalComponentConfig
    {
        protected readonly ComponentContext Context;
        protected readonly LocalComponentFactory Factory;

        #region Constructors

        public FluentLocalComponentConfig(ComponentContext context, Type componentType)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Factory = (LocalComponentFactory) ComponentContextUtils.CreateLocalFactory(componentType);
        }

        #endregion

        #region Fluent configuration methods

        public void Register(string contractName = null)
        {
            Context.Register(contractName, Factory);
        }

        public void RegisterWith<TContract>(string contractName = null)
        {
            RegisterWith(typeof(TContract), contractName);
        }

        public void RegisterWith(Type contractType, string contractName = null)
        {
            Context.Register(contractType, contractName, Factory);
        }

        public FluentLocalComponentConfig SetComponent<TPlugContract>(
            string memberName, string contractName = null, bool required = true)
        {
            return SetComponent(memberName, typeof(TPlugContract), contractName, required);
        }

        public FluentLocalComponentConfig SetComponent(
            string memberName, Type contractType, string contractName = null, bool required = true)
        {
            Factory.InitializationPoints.Add(new InitializationPointSpecification(memberName, MemberTypes.All,
                required, new ComponentQuery(contractType, contractName)));

            return this;
        }

        public FluentLocalComponentConfig AddConstructorComponent<TPlugContract>(string contractName = null, bool required = true)
        {
            return AddConstructorComponent(typeof(TPlugContract), contractName, required);
        }

        public FluentLocalComponentConfig AddConstructorComponent(Type contractType, string contractName = null, bool required = true)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig AddConstructorValue(object value)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig AddConstructorValue<TMember>(Func<IComposer, TMember> valueCalculator)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig AddConstructorValueFromVariable(string variableName)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig SetValue(string memberName, object value)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig SetValue<TMember>(string memberName, Func<IComposer, TMember> valueCalculator)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig SetValueFromVariable(string memberName, string variableName)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig NotifyInitialized(Action<IComposer, object> initAction)
        {
            Factory.CompositionNotificationMethods.Add(initAction);
            return this;
        }

        public FluentLocalComponentConfig UseComponentCache(Type cacheContractType, string cacheContractName = null)
        {
            if (cacheContractType == null)
                Factory.ComponentCacheQuery = new NullQuery();
            else
                Factory.ComponentCacheQuery = new ComponentQuery(cacheContractType, cacheContractName);

            return this;
        }

        public FluentLocalComponentConfig UseComponentCache<TCacheContract>(string cacheContractName = null)
        {
            return UseComponentCache(typeof(TCacheContract), cacheContractName);
        }

        #endregion
    }
}