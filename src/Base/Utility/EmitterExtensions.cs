using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Appson.Composer.Emitter;

namespace Appson.Composer.Utility
{
	public static class EmitterExtensions
	{
		public static Type EmitInterfaceImplementation(this IClassEmitter classEmitter,
		                                               Type interfaceType,
		                                               Type baseType = null,
		                                               ConstructorInfo baseConstructor = null,
		                                               IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
		{
			if (classEmitter == null)
				throw new ArgumentNullException(nameof(classEmitter));
			if (interfaceType == null)
				throw new ArgumentNullException(nameof(interfaceType));
			if (!interfaceType.IsInterface)
				throw new ArgumentException("interfaceType should point to an interface type definition.");

			return classEmitter.EmitClass(new[] {interfaceType}, baseType, null, null, null, baseConstructor,
			                              attributeBuilders);
		}

		public static T EmitInterfaceInstance<T>(this IClassEmitter classEmitter,
		                                         IEmittedTypeHandler handler,
		                                         Type baseType = null,
		                                         ConstructorInfo baseConstructor = null,
		                                         IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
			where T : class
		{
			if (classEmitter == null)
				throw new ArgumentNullException(nameof(classEmitter));

			var interfaceType = typeof (T);
			if (!interfaceType.IsInterface)
				throw new ArgumentException("T type argument should point to an interface type definition.");

			return classEmitter.EmitInterfaceInstance(handler, interfaceType, baseType, baseConstructor, attributeBuilders) as T;
		}

		public static object EmitInterfaceInstance(this IClassEmitter classEmitter,
		                                           IEmittedTypeHandler handler,
		                                           Type interfaceType,
		                                           Type baseType = null,
		                                           ConstructorInfo baseConstructor = null,
		                                           IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
		{
			if (classEmitter == null)
				throw new ArgumentNullException(nameof(classEmitter));
			if (interfaceType == null)
				throw new ArgumentNullException(nameof(interfaceType));
			if (!interfaceType.IsInterface)
				throw new ArgumentException("interfaceType should point to an interface type definition.");

			return classEmitter.EmitInstance(handler, new[] {interfaceType}, baseType, null, null, null, baseConstructor,
			                                 attributeBuilders);
		}

		public static object EmitInstance(this IClassEmitter classEmitter,
		                                  IEmittedTypeHandler handler,
		                                  IEnumerable<Type> interfaceTypes = null,
		                                  Type baseType = null,
		                                  IEnumerable<MethodInfo> methodsToOverride = null,
		                                  IEnumerable<PropertyInfo> propertiesToOverride = null,
		                                  IEnumerable<EventInfo> eventsToOverride = null,
		                                  ConstructorInfo baseConstructor = null,
		                                  IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
		{
			if (classEmitter == null)
				throw new ArgumentNullException(nameof(classEmitter));
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			var emittedType = classEmitter.EmitClass(interfaceTypes, baseType, methodsToOverride, propertiesToOverride,
			                                         eventsToOverride, baseConstructor, attributeBuilders);

			var result = Activator.CreateInstance(emittedType);
			if (result is IEmittedClass)
				((IEmittedClass) result).EmittedTypeHandler = handler;

			return result;
		}
	}
}