using System;

namespace Appson.Composer.Emitter
{
	/// <summary>
	/// Abstract base class for all dynamically generated classes
	/// </summary>
	public abstract class EmittedClassBase : IEmittedClass
	{
		private IEmittedTypeHandler _emittedTypeHandler;

		public IEmittedTypeHandler EmittedTypeHandler
		{
			set { _emittedTypeHandler = value; }
		}

		public virtual object DispatchCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType)
		{
			return _emittedTypeHandler.HandleCall(reflectedType, methodName, arguments, argumentTypes, resultType);
		}

        public virtual object DispatchPropertyGet(Type propertyOwner, string propertyName, Type propertyType)
        {
            return _emittedTypeHandler.HandlePropertyGet(propertyOwner, propertyName, propertyType);
        }

		public virtual void DispatchPropertySet(Type propertyOwner, string propertyName, Type propertyType, object newValue)
        {
            _emittedTypeHandler.HandlePropertySet(propertyOwner, propertyName, propertyType, newValue);
        }

		public virtual void DispatchEventSubscription(Type eventOwner, string eventName, Type eventType, Delegate target, bool subscribe)
        {
            _emittedTypeHandler.HandleEventSubscription(eventOwner, eventName, eventType, target, subscribe);
        }
	}
}