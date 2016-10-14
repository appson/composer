using System;

namespace Compositional.Composer
{
    public class ChildComponentContext : ComponentContext
    {
        private ComponentContext _parentContext;

        public ChildComponentContext(ComponentContext parentContext) : base(false)
        {
            if (parentContext == null)
                throw new ArgumentNullException("parentContext");

            _parentContext = parentContext;
        }


    }
}