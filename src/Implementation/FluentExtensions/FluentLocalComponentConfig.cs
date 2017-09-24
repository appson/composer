using System;

namespace Appson.Composer.FluentExtensions
{
    public class FluentLocalComponentConfig
    {
        private ComponentContext _context;
        private Type _componentType;

        #region Constructors

        public FluentLocalComponentConfig(ComponentContext context, Type componentType)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
        }

        #endregion

        #region Fluent configuration methods

        public void Register(string contractName = null)
        {
            // TODO
        }

        public void RegisterWith<TContract>(string contractName = null)
        {
            // TODO
        }

        public void RegisterWith(Type contractType, string contractName = null)
        {
            // TODO
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

        #endregion
    }
}