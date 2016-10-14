using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Compositional.Composer.Emitter
{
	[Component]
	[ComponentCache(null)]
	public class DefaultMethodEmitter : IMethodEmitter
	{
		#region Implementation of IMethodEmitter

		public MethodBuilder EmitMethod(TypeBuilder targetTypeBuilder,
		                                string methodName,
		                                Type[] parameterTypes,
		                                bool[] isParameterOut,
		                                Type resultType,
		                                Type reflectedType,
		                                MethodAttributes methodAttributes)
		{
			int parameterCount = parameterTypes.Count();

			var methodBuilder = targetTypeBuilder.DefineMethod(
				methodName,
				methodAttributes,
				resultType,
				parameterTypes.ToArray());

			var ilGenerator = methodBuilder.GetILGenerator();

			// C#: Type     reflectedType;
			// C#: object[] arguments;
			// C#: Type[]   argumentTypes;
			// C#: Type     resultType;

			var localReflectedType = ilGenerator.DeclareLocal(typeof (Type));
			var localArguments = ilGenerator.DeclareLocal(typeof (object[]));
			var localArgumentTypes = ilGenerator.DeclareLocal(typeof (Type[]));
			var localResultType = ilGenerator.DeclareLocal(typeof (Type));

			// C#: reflectedType = typeof(<interface>);

			ilGenerator.Emit(OpCodes.Ldtoken, reflectedType);
			ilGenerator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
			ilGenerator.Emit(OpCodes.Stloc, localReflectedType);

			// C#: arguments = new object[<paramCount>];

			ilGenerator.Emit(OpCodes.Ldc_I4, parameterCount);
			ilGenerator.Emit(OpCodes.Newarr, typeof (object));
			ilGenerator.Emit(OpCodes.Stloc, localArguments);

			// C#: argumentTypes = new Type[<paramCount>];

			ilGenerator.Emit(OpCodes.Ldc_I4, parameterCount);
			ilGenerator.Emit(OpCodes.Newarr, typeof (Type));
			ilGenerator.Emit(OpCodes.Stloc, localArgumentTypes);

			// C#: resultType = typeof(<result>);

			ilGenerator.Emit(OpCodes.Ldtoken, resultType);
			ilGenerator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
			ilGenerator.Emit(OpCodes.Stloc, localResultType);

			// Iterate through parameters to prepare the "paramArray" contents

			for (var i = 0; i < parameterCount; i++)
			{
				// When the parameter is out:
				// C#: arguments[<paramNum>] = null;
				// Otherwise:
				// C#: arguments[<paramNum>] = <param>;

				ilGenerator.Emit(OpCodes.Ldloc, localArguments);
				ilGenerator.Emit(OpCodes.Ldc_I4, i);

				// If the param is "out", then it is set to null and 
				// does not need any additional process. If it is a "ref"
				// parameter, the value should be de-referenced and placed
				// in the paramArray elements.

				var dereferencedParameterType =
					parameterTypes[i].IsByRef
						? parameterTypes[i].GetElementType()
						: parameterTypes[i];

				if (isParameterOut[i])
				{
					ilGenerator.Emit(OpCodes.Ldnull);
				}
				else
				{
					ilGenerator.Emit(OpCodes.Ldarg, i + 1);

					if (parameterTypes[i].IsByRef)
					{
						// De-referencing of value-type pointers and reference-type
						// pointers are performed in different ways.

						if (dereferencedParameterType.IsValueType)
							ilGenerator.Emit(OpCodes.Ldobj, dereferencedParameterType);
						else
							ilGenerator.Emit(OpCodes.Ldind_Ref);
					}

					// Should perform boxing if the parameter is a value type
					// regardless of when it's passed by reference or by value

					if (dereferencedParameterType.IsValueType)
						ilGenerator.Emit(OpCodes.Box, dereferencedParameterType);
				}

				ilGenerator.Emit(OpCodes.Stelem_Ref);

				// C#: argumentTypes[<paramNum>] = <Type of Parameter>;

				ilGenerator.Emit(OpCodes.Ldloc, localArgumentTypes);
				ilGenerator.Emit(OpCodes.Ldc_I4, i);

				if (parameterTypes[i].IsByRef)
				{
					// If the parameter is by-ref, can't create a token in IL for it.
					// So we have to place non-by-ref type, and then call Type.MakeByRefType
					// method to create the by-ref type in runtime.

					ilGenerator.Emit(OpCodes.Ldtoken, dereferencedParameterType);
					ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
					ilGenerator.Emit(OpCodes.Callvirt, typeof (Type).GetMethod("MakeByRefType"));
				}
				else
				{
					// The parameter type is not by-ref, so we can place it in a
					// token, use ldtoken, and then call Type.GetTypeFromHandle
					// to obtain the actual "Type" instance to put it in the array.

					ilGenerator.Emit(OpCodes.Ldtoken, parameterTypes[i]);
					ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
				}

				ilGenerator.Emit(OpCodes.Stelem_Ref);
			}

			// C#: DispatchCall(reflectedType, "<methodName>", arguments, argumentTypes, resultType);

			ilGenerator.Emit(OpCodes.Ldarg_0);
			ilGenerator.Emit(OpCodes.Ldloc, localReflectedType);
			ilGenerator.Emit(OpCodes.Ldstr, methodName);
			ilGenerator.Emit(OpCodes.Ldloc, localArguments);
			ilGenerator.Emit(OpCodes.Ldloc, localArgumentTypes);
			ilGenerator.Emit(OpCodes.Ldloc, localResultType);
			ilGenerator.Emit(OpCodes.Callvirt, typeof (IEmittedClass).GetMethod("DispatchCall"));

			// Iterate through parameters to set "ref" and "out" parameters

			for (var i = 0; i < parameterCount; i++)
			{
				// If the parameter is not "out" or "ref", there's no need
				// to further process the argument.

				if ((!isParameterOut[i]) && (!parameterTypes[i].IsByRef))
					continue;

				// Otherwise (if the parameter is out or ref):
				// C#: <param> = arguments[<paramNum>];

				var dereferencedParameterType =
					parameterTypes[i].IsByRef
						? parameterTypes[i].GetElementType()
						: parameterTypes[i];

				ilGenerator.Emit(OpCodes.Ldarg, i + 1);
				ilGenerator.Emit(OpCodes.Ldloc, localArguments);
				ilGenerator.Emit(OpCodes.Ldc_I4, i);
				ilGenerator.Emit(OpCodes.Ldelem_Ref);

				// If the type of parameter is a value-type, the loaded
				// object reference should be unboxed. If it's a reference
				// type, it should be cast to the target type.

				ilGenerator.Emit(dereferencedParameterType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass,
				                 dereferencedParameterType);

				ilGenerator.Emit(OpCodes.Stind_Ref);
			}

			// Prepare the return value based on the return type of the method

			if (resultType == typeof (void))
			{
				// If the return type is void, just remove the value at top of the
				// stack. (Ignore the value returned by DispatchCall)

				ilGenerator.Emit(OpCodes.Pop);
			}
			else if (resultType.IsValueType)
			{
				// If the return type is value type, we should check for null.
				// If the object returned from DispatchCall is not null, we 
				// should unbox it and return. If the value is null, the default
				// value of the return type should be returned instead.

				var notNullLabel = ilGenerator.DefineLabel();
				var endLabel = ilGenerator.DefineLabel();

				ilGenerator.Emit(OpCodes.Dup);
				ilGenerator.Emit(OpCodes.Brtrue_S, notNullLabel);

				ilGenerator.Emit(OpCodes.Pop);

				var resultBuilder = ilGenerator.DeclareLocal(resultType);
				ilGenerator.Emit(OpCodes.Ldloca_S, resultBuilder);
				ilGenerator.Emit(OpCodes.Initobj, resultType);

				ilGenerator.Emit(OpCodes.Ldloc, resultBuilder);
				ilGenerator.Emit(OpCodes.Br_S, endLabel);

				ilGenerator.MarkLabel(notNullLabel);
				ilGenerator.Emit(OpCodes.Unbox_Any, resultType);

				ilGenerator.MarkLabel(endLabel);
			}
			else
			{
				// If the return type is a reference type, it may need to be
				// cast to the appropriate type before returning.
				// The return type of DispatchCall is object, so perform the
				// cast if the return type of the method is not object.

				if (resultType != typeof (object))
					ilGenerator.Emit(OpCodes.Castclass, resultType);
			}

			ilGenerator.Emit(OpCodes.Ret);

			return methodBuilder;
		}

		#endregion
	}
}