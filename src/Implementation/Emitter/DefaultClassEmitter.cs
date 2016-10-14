using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Appson.Composer.Emitter
{
	[Component]
	[ComponentCache(null)]
	public class DefaultClassEmitter : IClassEmitter
	{
		private AssemblyBuilder _assemblyBuilder;
		private ModuleBuilder _moduleBuilder;
		private int _typeNumber;

		#region Configuration Points

		private string _dynamicAssemblyName = "Compositional.Composer.Dynamic";
		private string _dynamicTypePrefix = "Compositional.Composer.Dynamic.";

		[ConfigurationPoint(false)]
		public string DynamicAssemblyName
		{
			get { return _dynamicAssemblyName; }
			set { _dynamicAssemblyName = value; }
		}

		[ConfigurationPoint(false)]
		public string DynamicTypePrefix
		{
			get { return _dynamicTypePrefix; }
			set { _dynamicTypePrefix = value; }
		}

		[ConfigurationPoint(false)]
		public bool SaveEmittedAssembly { get; set; }

		[ConfigurationPoint(false)]
		public string SaveTargetFolder { get; set; }

		[ConfigurationPoint(false)]
		public bool EmitDebuggableAssembly { get; set; }

		#endregion

		#region Component Plugs

		[ComponentPlug]
		public IMethodEmitter MethodEmitter { get; set; }

		[ComponentPlug]
		public IPropertyEmitter PropertyEmitter { get; set; }

		[ComponentPlug]
		public IEventEmitter EventEmitter { get; set; }

		#endregion

		#region Constants

		private const MethodAttributes InterfaceMethodAttributes = MethodAttributes.Public
		                                                           | MethodAttributes.HideBySig
		                                                           | MethodAttributes.NewSlot
		                                                           | MethodAttributes.Virtual
		                                                           | MethodAttributes.Final;

		#endregion

		#region Construction Logic

		public DefaultClassEmitter()
		{
			SaveEmittedAssembly = false;
			_assemblyBuilder = null;
			_moduleBuilder = null;
			_typeNumber = 1;
		}

		[OnCompositionComplete]
		public void OnCompositionComplete()
		{
			if (SaveEmittedAssembly)
			{
				_assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
					new AssemblyName(DynamicAssemblyName),
					AssemblyBuilderAccess.RunAndSave,
					SaveTargetFolder);
			}
			else
			{
				_assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
					new AssemblyName(DynamicAssemblyName),
					AssemblyBuilderAccess.Run);
			}

			if (EmitDebuggableAssembly)
			{
				var debuggalbeAttributeBuilder = new CustomAttributeBuilder(
					typeof (DebuggableAttribute).GetConstructor(new[] {typeof (bool), typeof (bool)}),
					new object[] {true, true}
					);

				_assemblyBuilder.SetCustomAttribute(debuggalbeAttributeBuilder);
			}

			if (SaveEmittedAssembly)
			{
				_moduleBuilder = _assemblyBuilder.DefineDynamicModule(
					DynamicAssemblyName,
					DynamicAssemblyName + ".dll",
					true);
			}
			else
			{
				_moduleBuilder = _assemblyBuilder.DefineDynamicModule(
					DynamicAssemblyName,
					true);
			}
		}

		#endregion

		#region Implementation of IClassEmitter

		public Type EmitClass(IEnumerable<Type> interfaceTypes,
		                              Type baseType,
		                              IEnumerable<MethodInfo> methodsToOverride,
		                              IEnumerable<PropertyInfo> propertiesToOverride,
		                              IEnumerable<EventInfo> eventsToOverride,
		                              ConstructorInfo baseConstructor,
		                              IEnumerable<CustomAttributeBuilder> attributeBuilders)
		{
			// Apply default values for parameters

			if (baseType == null)
				baseType = typeof (EmittedClassBase);

			// Check limitations for this version

			if (baseType != typeof (EmittedClassBase))
				throw new NotImplementedException("The only supported baseType value is 'EmittedClassBase' in this version.");
			if ((interfaceTypes == null) || (interfaceTypes.Count() != 1))
				throw new NotImplementedException(
					"Current implementation should be provided with exactly one single interface to implement.");

			Type interfaceType = interfaceTypes.Single();

			// Validate arguments

			if (!interfaceType.IsInterface)
				throw new ArgumentException("interfaceTypes can contain 'interface' types only. " + interfaceType.FullName +
				                            " is not an interface.");

			// Check if the component is initialized

			if ((_moduleBuilder == null) || (_assemblyBuilder == null))
				throw new InvalidOperationException("The DefaultClassEmitter component is not initialized properly.");

			var uniqueTypeNumber = Interlocked.Increment(ref _typeNumber);
			var typeName = DynamicTypePrefix + interfaceType.Name + uniqueTypeNumber;

			var typeBuilder = _moduleBuilder.DefineType(
				typeName,
				TypeAttributes.Public
				| TypeAttributes.Class
				| TypeAttributes.AutoClass
				| TypeAttributes.AnsiClass
				| TypeAttributes.BeforeFieldInit
				| TypeAttributes.AutoLayout,
				typeof (EmittedClassBase),
				new[] {interfaceType});

			// Add custom attributes

			if (attributeBuilders != null)
			{
				foreach (var builder in attributeBuilders)
				{
					typeBuilder.SetCustomAttribute(builder);
				}
			}

			// Implement properties, events and methods

			foreach (var propertyInfo in interfaceType.GetProperties())
			{
				PropertyEmitter.EmitProperty(typeBuilder, propertyInfo);
			}

			foreach (var eventInfo in interfaceType.GetEvents())
			{
				EventEmitter.EmitEvent(typeBuilder, eventInfo);
			}

			foreach (var methodInfo in interfaceType.GetMethods())
			{
				MethodBuilder methodBuilder = MethodEmitter.EmitMethod(
					typeBuilder,
					methodInfo.Name,
					methodInfo.GetParameters().Select(pi => pi.ParameterType).ToArray(),
					methodInfo.GetParameters().Select(pi => pi.IsOut).ToArray(),
					methodInfo.ReturnType,
					methodInfo.ReflectedType,
					InterfaceMethodAttributes);

				typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
			}

			var result = typeBuilder.CreateType();


			if (SaveEmittedAssembly)
				_assemblyBuilder.Save(DynamicAssemblyName + ".dll");

			return result;
		}

		#endregion
	}
}