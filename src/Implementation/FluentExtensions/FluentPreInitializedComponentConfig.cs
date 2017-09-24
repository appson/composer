using System;

namespace Appson.Composer.FluentExtensions
{
    public class FluentPreInitializedComponentConfig
    {
        private ComponentContext _context;

        public object Instance { get; set; }

        #region Constructors

        public FluentPreInitializedComponentConfig(ComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region Fluent configuration methods

        public void Register(string contractName = null)
        {
            // TODO
        }

        public void RegisterWith<T>(string contractName = null)
        {
            // TODO
        }

        public void RegisterWith(Type contractType, string contractName = null)
        {
            // TODO
        }

        #endregion

    }
}