using System;
using System.Linq;
using Compositional.Composer.Emitter;

namespace Compositional.Composer.Samples.Basic
{
	public class TestEmittedTypeHandler : IEmittedTypeHandler
	{
		#region Implementation of IEmittedTypeHandler

		public object HandleCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType)
		{
			Console.WriteLine("INVOCATION   - TestEmittedTypeHandler.HandleCall");
			Console.WriteLine("    reflectedType = {0}", reflectedType.Name);
			Console.WriteLine("    methodName    = {0}", methodName);
			Console.WriteLine("    argumentTypes = {0}", string.Join(", ", argumentTypes.Select(t => t.Name)));
			Console.WriteLine("    arguments     = {0}", string.Join(", ", arguments.Select(t => t.ToString())));
			Console.WriteLine("    resultType    = {0}", resultType.Name);

			switch (methodName)
			{
				case "Add":
					return ((int) arguments[0]) + ((int) arguments[1]);

				case "Multiply":
					return ((int)arguments[0]) * ((int)arguments[1]);

				case "Divide":
					return ((int)arguments[0]) / ((int)arguments[1]);

				case "Remainder":
					return ((int)arguments[0]) % ((int)arguments[1]);
			}

			throw new ArgumentException("Unsupported method: " + methodName);
		}

		public object HandlePropertyGet(Type reflectedType, string propertyName, Type propertyType)
		{
			throw new NotImplementedException();
		}

		public void HandlePropertySet(Type reflectedType, string propertyName, Type propertyType, object newValue)
		{
			throw new NotImplementedException();
		}

		public void HandleEventSubscription(Type reflectedType, string eventName, Type eventType, Delegate target, bool subscribe)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
