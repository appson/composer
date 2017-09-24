using System;
using Appson.Composer.Factories;

namespace Appson.Composer.FluentExtensions
{
    public class FluentLocalComponentConfig
    {
        private readonly ComponentContext _context;
        private readonly LocalComponentFactory _factory;

        #region Constructors

        public FluentLocalComponentConfig(ComponentContext context, Type componentType)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _factory = (LocalComponentFactory) ComponentContextUtils.CreateLocalFactory(componentType);
        }

        #endregion

        #region Fluent configuration methods

        public void Register(string contractName = null)
        {
            _context.Register(contractName, _factory);
        }

        public void RegisterWith<TContract>(string contractName = null)
        {
            RegisterWith(typeof(TContract), contractName);
        }

        public void RegisterWith(Type contractType, string contractName = null)
        {
            _context.Register(contractType, contractName, _factory);
        }

        public FluentLocalComponentConfig SetComponent<TPlugContract>(
            string memberName, string contractName = null, bool required = true)
        {
            return SetComponent(memberName, typeof(TPlugContract), contractName, required);
        }

        public FluentLocalComponentConfig SetComponent(
            string memberName, Type contractType, string contractName = null, bool required = true)
        {
            // TODO
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

        public FluentLocalComponentConfig NotifyInitialized(string methodName)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig NotifyInitialized(Action<IComposer> initAction)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig UseComponentCache(Type cacheContractType, string cacheContractName = null)
        {
            // TODO
            return this;
        }

        public FluentLocalComponentConfig UseComponentCache<TCacheContract>(string cacheContractName = null)
        {
            // TODO
            return this;
        }

        #endregion
    }
}