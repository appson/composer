//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Reflection.Emit;
//using System.Threading;
//
//namespace Compositional.Composer.Emitter
//{
	/// <summary>
	/// Creates dynamic implementations for interfaces, which dispatch every method
	/// call to an instance of IEmittedTypeHandler interface.
	/// </summary>
//	public static class DynamicImplementationBuilder
//	{
//		private const string DynamicAssemblyName = "Compositional.Composer.Dynamic";
//		private const string DynamicTypePrefix = "Compositional.Composer.Dynamic.";
//
//		private const MethodAttributes InterfaceMethodAttributes =
//			MethodAttributes.Public
//			| MethodAttributes.HideBySig
//			| MethodAttributes.NewSlot
//			| MethodAttributes.Virtual
//			| MethodAttributes.Final;
//
//		private static readonly AssemblyBuilder assemblyBuilder;
//		private static readonly ModuleBuilder moduleBuilder;
//		private static int _typeNumber;
//
//		static DynamicImplementationBuilder()
//		{
//			assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
//				new AssemblyName(DynamicAssemblyName),
//				AssemblyBuilderAccess.Run);
//
//			moduleBuilder = assemblyBuilder.DefineDynamicModule(DynamicAssemblyName, true);
//
			// FOR SAVE, replace above lines with:
			//assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
			//	new AssemblyName(DynamicAssemblyName),
			//	AssemblyBuilderAccess.RunAndSave);
			//
			//moduleBuilder = assemblyBuilder.DefineDynamicModule(DynamicAssemblyName, "dynamic.dll", true);
//		}
//
//		public static T BuildDynamicInterfaceImplementation<T>(IEmittedTypeHandler dynamicTypeHandler,
//		                                                       IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
//			where T : class // Only interface type if permitted here
//		{
//			return (T) BuildDynamicInterfaceImplementation(typeof (T), dynamicTypeHandler, attributeBuilders);
//		}
//
//	    public static object BuildDynamicInterfaceImplementation(Type interfaceType, IEmittedTypeHandler dynamicTypeHandler,
//		                                                         IEnumerable<CustomAttributeBuilder> attributeBuilders = null)
//		{
//			if (!interfaceType.IsInterface)
//				throw new ArgumentException("Generic parameter can only be an interface type.");
//
//			var uniqueTypeNumber = Interlocked.Increment(ref _typeNumber);
//			var typeName = DynamicTypePrefix + interfaceType.Name + uniqueTypeNumber;
//
//			var typeBuilder = moduleBuilder.DefineType(
//				typeName,
//				TypeAttributes.Public
//				| TypeAttributes.Class
//				| TypeAttributes.AutoClass
//				| TypeAttributes.AnsiClass
//				| TypeAttributes.BeforeFieldInit
//				| TypeAttributes.AutoLayout,
//				typeof (EmittedClassBase),
//				new[] {interfaceType});
//
			// Add custom attributes
//
//			if (attributeBuilders != null)
//			{
//				foreach (var builder in attributeBuilders)
//				{
//					typeBuilder.SetCustomAttribute(builder);
//				}
//			}
//
			// Implement properties, events and methods
//
//			foreach (var propertyInfo in interfaceType.GetProperties())
//			{
//				 ImplementProperty(typeBuilder, propertyInfo);
//			}
//
//	        foreach (var eventInfo in interfaceType.GetEvents())
//	        {
//                ImplementEvent(typeBuilder, eventInfo);
//            }
//            
//            foreach (var methodInfo in interfaceType.GetMethods())
//			{
//				ImplementMethod(typeBuilder, methodInfo);
//			}
//
//			var builtType = typeBuilder.CreateType();
//
			// FOR SAVE: assemblyBuilder.Save("dynamic.dll");
//
//			var builtInstance = Activator.CreateInstance(builtType);
//
//			((EmittedClassBase) builtInstance).EmittedTypeHandler = dynamicTypeHandler;
//
//			return builtInstance;
//		}
//
//        private static void ImplementProperty(TypeBuilder typeBuilder, PropertyInfo propertyInfo)
//        {
            // TODO: Add property building functionality
//            throw new NotImplementedException(
//                "Interfaces that define some properties in them are not currently supported.");
//        }
//
//        private static void ImplementEvent(TypeBuilder typeBuilder, EventInfo eventInfo)
//        {
            // TODO: Add event building functionality
//            throw new NotImplementedException(
//                "Interfaces that define some events in them are not currently supported.");
//        }
//
//		private static void ImplementMethod(TypeBuilder typeBuilder, MethodInfo methodInfo)
//		{
//			var parameterTypeList = Array.ConvertAll(
//				methodInfo.GetParameters(),
//				info => info.ParameterType);
//
//			var methodBuilder = typeBuilder.DefineMethod(
//				methodInfo.Name,
//				InterfaceMethodAttributes,
//				methodInfo.ReturnType,
//				parameterTypeList);
//
//			var ilGenerator = methodBuilder.GetILGenerator();
//
			// C#: object[] paramArray;
			// C#: Type[]   paramTypes;
			// C#: Type     methodOwner;
//
//			var paramArray = ilGenerator.DeclareLocal(typeof (object[]));
//			var paramTypes = ilGenerator.DeclareLocal(typeof (Type[]));
//			var methodOwner = ilGenerator.DeclareLocal(typeof (Type));
//
			// C#: paramArray = new object[<paramCount>];
//
//			ilGenerator.Emit(OpCodes.Ldc_I4, methodInfo.GetParameters().Length);
//			ilGenerator.Emit(OpCodes.Newarr, typeof (object));
//			ilGenerator.Emit(OpCodes.Stloc, paramArray);
//
			// C#: paramTypes = new Type[<paramCount>];
//
//			ilGenerator.Emit(OpCodes.Ldc_I4, methodInfo.GetParameters().Length);
//			ilGenerator.Emit(OpCodes.Newarr, typeof (Type));
//			ilGenerator.Emit(OpCodes.Stloc, paramTypes);
//
			// C#: methodOwner = typeof(<interface>);
//
//			ilGenerator.Emit(OpCodes.Ldtoken, methodInfo.ReflectedType);
//			ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
//			ilGenerator.Emit(OpCodes.Stloc, methodOwner);
//
			// Iterate through parameters to prepare the "paramArray" contents
//
//			var methodParameters = methodInfo.GetParameters();
//			for (var i = 0; i < methodParameters.Length; i++)
//			{
				// When the parameter is out:
				// C#: paramArray[<paramNum>] = null;
				// Otherwise:
				// C#: paramArray[<paramNum>] = <param>;
//
//				ilGenerator.Emit(OpCodes.Ldloc, paramArray);
//				ilGenerator.Emit(OpCodes.Ldc_I4, i);
//
//				if (methodParameters[i].IsOut)
//					ilGenerator.Emit(OpCodes.Ldnull);
//				else
//					ilGenerator.Emit(OpCodes.Ldarg, i + 1);
//
				// If the param is "out", then it is set to null and 
				// does not need any additional process. If it is a "ref"
				// parameter, the value should be de-referenced and placed
				// in the paramArray elements.
//
//				var dereferencedParameterType =
//					methodParameters[i].ParameterType.IsByRef
//						?
//							methodParameters[i].ParameterType.GetElementType()
//						:
//							methodParameters[i].ParameterType;
//
//				if ((methodParameters[i].ParameterType.IsByRef) && (!methodParameters[i].IsOut))
//				{
					// De-referencing of value-type pointers and reference-type
					// pointers are performed in different ways.
//
//					if (dereferencedParameterType.IsValueType)
//						ilGenerator.Emit(OpCodes.Ldobj, dereferencedParameterType);
//					else
//						ilGenerator.Emit(OpCodes.Ldind_Ref);
//				}
//
				// Should perform boxing if the parameter is a value type
				// regardless of when it's passed by reference or by value
//
//				if (dereferencedParameterType.IsValueType)
//					ilGenerator.Emit(OpCodes.Box, dereferencedParameterType);
//
//				ilGenerator.Emit(OpCodes.Stelem_Ref);
//
				// C#: paramTypes[<paramNum>] = <Type of Parameter>;
//
//				ilGenerator.Emit(OpCodes.Ldloc, paramTypes);
//				ilGenerator.Emit(OpCodes.Ldc_I4, i);
//
//				if (methodParameters[i].ParameterType.IsByRef)
//				{
					// If the parameter is by-ref, can't create a token in IL for it.
					// So we have to place non-by-ref type, and then call Type.MakeByRefType
					// method to create the by-ref type in runtime.
//
//					ilGenerator.Emit(OpCodes.Ldtoken, dereferencedParameterType);
//					ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
//					ilGenerator.Emit(OpCodes.Callvirt, typeof (Type).GetMethod("MakeByRefType"));
//				}
//				else
//				{
					// The parameter type is not by-ref, so we can place it in a
					// token, use ldtoken, and then call Type.GetTypeFromHandle
					// to obtain the actual "Type" instance to put it in the array.
//
//					ilGenerator.Emit(OpCodes.Ldtoken, methodParameters[i].ParameterType);
//					ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
//				}
//
//				ilGenerator.Emit(OpCodes.Stelem_Ref);
//			}
//
			// C#: DispatchCall(methodOwner, "<methodName>", paramArray, paramByRefs);
//
//			ilGenerator.Emit(OpCodes.Ldarg_0);
//			ilGenerator.Emit(OpCodes.Ldloc, methodOwner);
//			ilGenerator.Emit(OpCodes.Ldstr, methodInfo.Name);
//			ilGenerator.Emit(OpCodes.Ldloc, paramArray);
//			ilGenerator.Emit(OpCodes.Ldloc, paramTypes);
//			ilGenerator.Emit(OpCodes.Call,
//			                 typeof (EmittedClassBase).GetMethod("DispatchCall",
//			                                                              BindingFlags.Instance | BindingFlags.NonPublic));
//
			// Iterate through parameters to set "ref" and "out" parameters
//
//			for (var i = 0; i < methodParameters.Length; i++)
//			{
				// If the parameter is not "out" or "ref", there's no need
				// to further process the argument.
//
//				if ((!methodParameters[i].IsOut) && (!methodParameters[i].ParameterType.IsByRef))
//					continue;
//
				// Otherwise (if the parameter is out or ref):
				// C#: <param> = paramArray[<paramNum>];
//
//				var dereferencedParameterType =
//					methodParameters[i].ParameterType.IsByRef
//						?
//							methodParameters[i].ParameterType.GetElementType()
//						:
//							methodParameters[i].ParameterType;
//
//				ilGenerator.Emit(OpCodes.Ldarg, i + 1);
//				ilGenerator.Emit(OpCodes.Ldloc, paramArray);
//				ilGenerator.Emit(OpCodes.Ldc_I4, i);
//				ilGenerator.Emit(OpCodes.Ldelem_Ref);
//
				// If the type of parameter is a value-type, the loaded
				// object reference should be unboxed. If it's a reference
				// type, it should be cast to the target type.
//
//				ilGenerator.Emit(dereferencedParameterType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass,
//				                 dereferencedParameterType);
//
//				ilGenerator.Emit(OpCodes.Stind_Ref);
//			}
//
			// Prepare the return value based on the return type of the method
//
//			if (methodInfo.ReturnType == typeof (void))
//			{
				// If the return type is void, just remove the value at top of the
				// stack. (Ignore the value returned by DispatchCall)
//
//				ilGenerator.Emit(OpCodes.Pop);
//			}
//			else if (methodInfo.ReturnType.IsValueType)
//			{
				// If the return type is value type, we should check for null.
				// If the object returned from DispatchCall is not null, we 
				// should unbox it and return. If the value is null, the default
				// value of the return type should be returned instead.
//
//				var notNullLabel = ilGenerator.DefineLabel();
//				var endLabel = ilGenerator.DefineLabel();
//
//				ilGenerator.Emit(OpCodes.Dup);
//				ilGenerator.Emit(OpCodes.Brtrue_S, notNullLabel);
//
//				ilGenerator.Emit(OpCodes.Pop);
//
//				var resultBuilder = ilGenerator.DeclareLocal(methodInfo.ReturnType);
//				ilGenerator.Emit(OpCodes.Ldloca_S, resultBuilder);
//				ilGenerator.Emit(OpCodes.Initobj, methodInfo.ReturnType);
//
//				ilGenerator.Emit(OpCodes.Ldloc, resultBuilder);
//				ilGenerator.Emit(OpCodes.Br_S, endLabel);
//
//				ilGenerator.MarkLabel(notNullLabel);
//				ilGenerator.Emit(OpCodes.Unbox_Any, methodInfo.ReturnType);
//
//				ilGenerator.MarkLabel(endLabel);
//			}
//			else
//			{
				// If the return type is a reference type, it may need to be
				// cast to the appropriate type before returning.
				// The return type of DispatchCall is object, so perform the
				// cast if the return type of the method is not object.
//
//				if (methodInfo.ReturnType != typeof (object))
//					ilGenerator.Emit(OpCodes.Castclass, methodInfo.ReturnType);
//			}
//
//			ilGenerator.Emit(OpCodes.Ret);
//
//			typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
//		}
//	}
//}