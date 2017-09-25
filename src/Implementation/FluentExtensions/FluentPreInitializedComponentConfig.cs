using System;
using Appson.Composer.Factories;

namespace Appson.Composer.FluentExtensions
{
    public class FluentPreInitializedComponentConfig
    {
        private readonly ComponentContext _context;
        private readonly PreInitializedComponentFactory _factory;

        #region Constructors

        public FluentPreInitializedComponentConfig(ComponentContext context, object componentInstance)
        {
            _context = context;
            _factory = new PreInitializedComponentFactory(componentInstance);
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

        #endregion

    }
}