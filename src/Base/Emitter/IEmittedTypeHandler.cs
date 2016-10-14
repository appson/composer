using System;

namespace Appson.Composer.Emitter
{
	/// <summary>
	/// Specifies the type interface which can handle any method calls dynamically.
	/// Used when building dynamic implementations of the classes, where each method
	/// call is redirected to a single HandleCall method call defined by this interface.
	/// </summary>
	public interface IEmittedTypeHandler
	{
		object HandleCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType);
	    
        object HandlePropertyGet(Type reflectedType, string propertyName, Type propertyType);
	    void HandlePropertySet(Type reflectedType, string propertyName, Type propertyType, object newValue);

	    void HandleEventSubscription(Type reflectedType, string eventName, Type eventType, Delegate target, bool subscribe);
	}
}