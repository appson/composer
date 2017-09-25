using System;
using System.Linq.Expressions;
using System.Reflection;
using Appson.Composer.CompositionalQueries;

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
            base.SetComponent(memberName, contractType, contractName, required);
            return this;
        }

        public FluentLocalComponentConfig<TComponent> SetComponent<TPlugContract>(
            Expression<Func<TComponent, TPlugContract>> member, string contractName = null, bool required = true)
        {
            if (!(member.Body is MemberExpression memberExpression) ||
                !(memberExpression.Expression is ParameterExpression parameterExpression) ||
                parameterExpression.Type != typeof(TComponent))
            {
                throw new ArgumentException("Member pointer should point to an immediate member. " +
                                            "The only acceptable expression format is <x => x.MemberName>.");
            }

            Factory.InitializationPoints.Add(new InitializationPointSpecification(memberExpression.Member.Name, memberExpression.Member.MemberType,
                required, new ComponentQuery(typeof(TPlugContract), contractName)));

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

        public FluentLocalComponentConfig<TComponent> NotifyInitialized(Action<IComposer, TComponent> initAction)
        {
            base.NotifyInitialized((c, o) => initAction(c, (TComponent)o));
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> UseComponentCache(Type cacheContractType, string cacheContractName = null)
        {
            base.UseComponentCache(cacheContractType, cacheContractName);
            return this;
        }

        public new FluentLocalComponentConfig<TComponent> UseComponentCache<TCacheContract>(string cacheContractName = null)
        {
            base.UseComponentCache<TCacheContract>(cacheContractName);
            return this;
        }

        #endregion

    }
}