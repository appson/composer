using System;

namespace Appson.Composer.Emitter
{
	public interface IEmittedClass
	{
		IEmittedTypeHandler EmittedTypeHandler { set; }

		object DispatchCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType);
		object DispatchPropertyGet(Type propertyOwner, string propertyName, Type propertyType);
		void DispatchPropertySet(Type propertyOwner, string propertyName, Type propertyType, object newValue);
		void DispatchEventSubscription(Type eventOwner, string eventName, Type eventType, Delegate target, bool subscribe);
	}
}
