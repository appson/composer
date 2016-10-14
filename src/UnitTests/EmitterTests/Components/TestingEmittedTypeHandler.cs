using System;
using Compositional.Composer.Emitter;

namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	internal class TestingEmittedTypeHandler : IEmittedTypeHandler
	{
		public Type ReflectedType { get; private set; }
		public string MemberName { get; private set; }

		public object[] CallArguments { get; private set; }
		public Type[] CallArgumentTypes { get; private set; }
		public Type CallResultType { get; private set; }

		public Type PropertyType { get; private set; }
		public object PropertyNewValue { get; private set; }

		public Type EventType { get; private set; }
		public Delegate EventTarget { get; private set; }
		public bool EventSubscribe { get; private set; }

		public object ReturnValue { get; set; }
		public object[] CallArgumentReplacements { get; set; }

		#region Implementation of IEmittedTypeHandler

		public object HandleCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType)
		{
			ReflectedType = reflectedType;
			MemberName = methodName;

			CallArguments = arguments;
			CallArgumentTypes = argumentTypes;
			CallResultType = resultType;

			if (CallArgumentReplacements != null)
			{
				CallArguments = new object[arguments.Length];
				Array.Copy(arguments, CallArguments, arguments.Length);

				for (int i = 0; i < CallArgumentReplacements.Length; i++)
					arguments[i] = CallArgumentReplacements[i];
				
			}
			return ReturnValue;
		}

		public object HandlePropertyGet(Type reflectedType, string propertyName, Type propertyType)
		{
			ReflectedType = reflectedType;
			MemberName = propertyName;

			PropertyType = propertyType;

			return ReturnValue;
		}

		public void HandlePropertySet(Type reflectedType, string propertyName, Type propertyType, object newValue)
		{
			ReflectedType = reflectedType;
			MemberName = propertyName;

			PropertyType = propertyType;
			PropertyNewValue = newValue;
		}

		public void HandleEventSubscription(Type reflectedType, string eventName, Type eventType, Delegate target, bool subscribe)
		{
			ReflectedType = reflectedType;
			MemberName = eventName;

			EventType = eventType;
			EventTarget = target;
			EventSubscribe = subscribe;
		}

		#endregion
	}
}
