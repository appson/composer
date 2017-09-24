using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Linq;
using Appson.Composer.CompositionalQueries;
using Appson.Composer.Factories;


namespace Appson.Composer
{
	public static class ComponentContextUtils
	{
		public static bool HasComponentAttribute(Type component)
		{
			return component.GetCustomAttributes(typeof(ComponentAttribute), false).Length > 0;
		}

		public static bool HasIgnoredOnAssemblyRegistrationAttribute(Type component)
		{
			return component.GetCustomAttributes(typeof(IgnoredOnAssemblyRegistrationAttribute), false).Length > 0;
		}

		public static bool HasComponentPlugAttribute(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(ComponentPlugAttribute), false).Length > 0;
		}

		public static bool HasResourceManagerPlugAttribute(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(ResourceManagerPlugAttribute), false).Length > 0;
		}

		public static bool HasConfigurationPointAttribute(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(ConfigurationPointAttribute), false).Length > 0;
		}

		public static bool IsInitializationPoint(MemberInfo memberInfo)
		{
			return
				HasComponentPlugAttribute(memberInfo) ||
				HasResourceManagerPlugAttribute(memberInfo) ||
				HasConfigurationPointAttribute(memberInfo);
		}

		public static bool HasOnCompositionCompleteAttribute(MethodInfo methodInfo)
		{
			return methodInfo.GetCustomAttributes(typeof(OnCompositionCompleteAttribute), false).Length > 0;
		}

		public static bool HasCompositionConstructorAttribute(ConstructorInfo constructorInfo)
		{
			return constructorInfo.GetCustomAttributes(typeof(CompositionConstructorAttribute), false).Length > 0;
		}

		public static bool HasContractAttribute(Type contract)
		{
			return (contract.GetCustomAttributes(typeof(ContractAttribute), false).Length > 0);
		}

		internal static void ThrowIfNotContract(Type contract)
		{
			// TODO: Move throwing to usage, provide a better message. (why should be a [contract])
			if (!HasContractAttribute(contract))
				throw new CompositionException(
				        $"The type {contract.FullName} should be marked with [Contract] attribute.");
		}

		internal static void ThrowIfNotSubTypeOf(Type contract, Type component)
		{
		    if (contract == component)
		        return;

			if (!component.IsSubclassOf(contract) && component.GetInterface(contract.Name) == null)
				throw new CompositionException(
				        $"Component type '{component.FullName}' is not a sub-type of contract '{contract.FullName}");
		}

		internal static ComponentCacheAttribute GetComponentCacheAttribute(Type component)
		{
			var attributes = component.GetCustomAttributes(typeof(ComponentCacheAttribute), false);

			if ((attributes.Length != 1) || !(attributes[0] is ComponentCacheAttribute))
				return null;

			return (ComponentCacheAttribute)attributes[0];
		}

		internal static string GetComponentDefaultName(Type component)
		{
			var attributes = component.GetCustomAttributes(typeof(ComponentAttribute), false);

			if ((attributes.Length != 1) || !(attributes[0] is ComponentAttribute))
				return null;

			return ((ComponentAttribute)attributes[0]).DefaultName;
		}

		internal static void CheckAndAddInitializationPoint(IComposer composer,
															List<InitializationPointSpecification> initializationPoints,
															MemberInfo memberInfo)
		{

			// Check if the member is already in the component configuration,
			// and if so, check if the member has the appropriate plug attribute

			if (initializationPoints.Any(i => i.Name == memberInfo.Name))
			{
				if (!composer.Configuration.DisableAttributeChecking && !IsInitializationPoint(memberInfo))
					throw new CompositionException(
					        $"The member '{memberInfo.Name}' of type '{memberInfo.ReflectedType?.FullName}' is in the list of initialization points, " +
					        "but it doesn't have any of the attributes for an initialization point associated.");

				// Ignore the member if it is already in the
				// initialization points of the component configuration

				return;
			}
			
			// Ignore the member if none of the initialization point
			// attributes are associated to it.

			if (!IsInitializationPoint(memberInfo))
				return;

			// Check if the member is a public field, or a property
			// with a public setter.

			EnsureWritable(memberInfo);

			// If the member is a component plug, prepare and 
			// add the initializtion point based on plug info.
			// If it is a configuration point, prepare the
			// initialization point accordingly.

			if (HasComponentPlugAttribute(memberInfo))
			{
				// Member is a new "ComponentPlug"

				Type contractType;

				switch (memberInfo.MemberType)
				{
					case MemberTypes.Field:
						contractType = ((FieldInfo)memberInfo).FieldType;
						break;

					case MemberTypes.Property:
						contractType = ((PropertyInfo)memberInfo).PropertyType;
						break;

					default:
						throw new CompositionException(
						    $"Member '{memberInfo.Name}' of type '{memberInfo.ReflectedType?.FullName}' cannot be marked with a [ComponentPlug] attribute." +
						    "An initialization point should either be a field or a property.");
				}

				if (!composer.Configuration.DisableAttributeChecking && !HasContractAttribute(contractType))
					throw new CompositionException("Component Plug '" + memberInfo.Name + "' on type '" +
					                               memberInfo.ReflectedType?.FullName + "' is of type '" + contractType.FullName +
					                               "' which is not a contract (is not marked with [Contract])");

				initializationPoints.Add(
					new InitializationPointSpecification(memberInfo.Name,
					                                     memberInfo.MemberType,
					                                     GetComponentPlugAttribute(memberInfo).Required,
					                                     new ComponentQuery(contractType,
					                                                        GetComponentPlugAttribute(memberInfo).Name)));
			}
			else if (HasResourceManagerPlugAttribute(memberInfo))
			{
				// Member is a new "ResourceManagerPlug"

				Type resourceManagerPlugType;

				switch (memberInfo.MemberType)
				{
					case MemberTypes.Field:
						resourceManagerPlugType = ((FieldInfo)memberInfo).FieldType;
						break;

					case MemberTypes.Property:
						resourceManagerPlugType = ((PropertyInfo)memberInfo).PropertyType;
						break;

					default:
						throw new CompositionException(
						    $"Member '{memberInfo.Name}' of type '{memberInfo.ReflectedType?.FullName}' cannot be marked with a [ResourcePlug] attribute." +
						    "An initialization point should either be a field or a property.");
				}

				if (resourceManagerPlugType != typeof(ResourceManager))
					throw new CompositionException("The ResourceManagerPlug named '" + memberInfo.Name + "' in the type '" +
					                               memberInfo.ReflectedType?.FullName + "' is of type '" +
					                               resourceManagerPlugType.FullName +
					                               "', where resource plugs can only be of type 'ResourceManager'.");

				var resourceId = GetResourceManagerPlugAttribute(memberInfo).Id;

				if (resourceId == null)
					throw new CompositionException("The ResourceManagerPlug named '" + memberInfo.Name + "' in the type '" +
												   memberInfo.ReflectedType?.FullName + "' does not have an Id associated with it.");

				initializationPoints.Add(
					new InitializationPointSpecification(memberInfo.Name,
					                                     memberInfo.MemberType,
					                                     GetResourceManagerPlugAttribute(memberInfo).Required,
					                                     new ResourcePlugQuery(resourceId)));
			}
			else
			{
				// Member is a new "ConfigurationPoint"

				var configurationPointAttribute = GetConfigurationPointAttribute(memberInfo);

				if (string.IsNullOrEmpty(configurationPointAttribute.ConfigurationVariableName))
				{
					// If the variable name is not provided and the configuration point
					// is required, throw.

					if (configurationPointAttribute.Required)
						throw new CompositionException(
							"Configuration points marked as required should either have a variable name set, or be initialized in the component configuration by caller.");

					// If it is not provided, ignore and don't add it to the
					// list of initialization points.
				}
				else
				{
					// If the name of the variable is provided, add it
					// to the initialization points list by creating a
					// reference to the variable.

					initializationPoints.Add(
						new InitializationPointSpecification(memberInfo.Name,
						                                     memberInfo.MemberType,
						                                     configurationPointAttribute.Required,
						                                     new VariableQuery(configurationPointAttribute.ConfigurationVariableName)));
				}
			}
		}

		private static void EnsureWritable(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				var fieldInfo = (FieldInfo)memberInfo;

				if (!fieldInfo.IsPublic)
					throw new CompositionException("Initialization point '" + memberInfo.Name + "' of type '" +
					                               memberInfo.ReflectedType?.FullName + "' is not public, and can not be set.");

				return;
			}

			if (memberInfo.MemberType == MemberTypes.Property)
			{
				var propertyInfo = (PropertyInfo) memberInfo;

				if ((!propertyInfo.CanWrite) || (propertyInfo.GetSetMethod() == null))
					throw new CompositionException("Initialization point '" + memberInfo.Name + "' of type '" +
					                               memberInfo.ReflectedType?.FullName + "' does not have a public setter.");

				return;
			}

			throw new CompositionException("Initialization point '" + memberInfo.Name + "' of type '" +
										   memberInfo.ReflectedType?.FullName + "' is not a writable field or property.");
		}

		internal static bool IsOnCompositionCompleteMethod(MemberInfo memberInfo)
		{
			// Check if the member is a method. If not, return false.

			var methodInfo = memberInfo as MethodInfo;

			if (methodInfo == null)
				return false;

			// Check if the member has the [OnCompositionComplete] attribute

			if (!HasOnCompositionCompleteAttribute(methodInfo))
				return false;

			// Check if the method qualifies as a [OnCompositionComplete] method.
			// This means that the method has a 'void' return value, and does
			// not require any parameters.

			// If the condition is not met, it means that the attribute is
			// placed on an inappropriate method, so throw an exception.

			if (methodInfo.ReturnType != typeof(void))
				throw new CompositionException(
				        $"Return type of the method has to be 'void' to be an [OnCompositionComplete] method. Method '{methodInfo.Name}' in type '{methodInfo.DeclaringType?.FullName}' is marked with [OnCompositionComplete] but the return type is '{methodInfo.ReturnType.FullName}'.");

			if (methodInfo.GetParameters().Length > 0)
				throw new CompositionException(
				        $"A method should not receive any arguments to be an [OnCompositionComplete] method. Method '{methodInfo.Name}' in type '{methodInfo.DeclaringType?.FullName}' is marked with [OnCompositionComplete] but expects '{methodInfo.GetParameters().Length}' arguments.");

			// Set the member as the [OnCompositionComplete] method on the component configuration.

			return true;
		}

		internal static ComponentPlugAttribute GetComponentPlugAttribute(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(ComponentPlugAttribute), false)[0] as ComponentPlugAttribute;
		}

		internal static ResourceManagerPlugAttribute GetResourceManagerPlugAttribute(MemberInfo memberInfo)
		{
			return
				memberInfo.GetCustomAttributes(typeof(ResourceManagerPlugAttribute), false)[0] as ResourceManagerPlugAttribute;
		}

		internal static ConfigurationPointAttribute GetConfigurationPointAttribute(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttributes(typeof(ConfigurationPointAttribute), false)[0] as ConfigurationPointAttribute;
		}

		internal static CompositionConstructorAttribute GetCompositionConstructorAttribute(MemberInfo memberInfo)
		{
			return
				memberInfo.GetCustomAttributes(typeof(CompositionConstructorAttribute), false)[0] as
				CompositionConstructorAttribute;
		}

		internal static IEnumerable<Type> FindContracts(Type type)
		{
			return FindContractsFromBaseClasses(type).Concat(FindContractsFromInterfaces(type)).Distinct();
		}

		private static IEnumerable<Type> FindContractsFromInterfaces(Type type)
		{
			return type.GetInterfaces().Where(HasContractAttribute);
		}

		private static IEnumerable<Type> FindContractsFromBaseClasses(Type type)
		{
			Type candidateContract = type;
			var result = new List<Type>();

			while (candidateContract != null)
			{
				if (HasContractAttribute(candidateContract))
					result.Add(candidateContract);

				candidateContract = candidateContract.BaseType;
			}

			return result;
		}

		internal static IEnumerable<InitializationPointSpecification> ExtractInitializationPoints(IComposer composer, Type type)
		{
			var result = new List<InitializationPointSpecification>();

			foreach (var memberInfo in type.GetMembers())
				CheckAndAddInitializationPoint(composer, result, memberInfo);

			return result;
		}

		internal static IEnumerable<MethodInfo> FindCompositionNotificationMethods(Type targetType)
		{
			var result = new List<MethodInfo>();

			// There are two possibilities defining notification methods.
			//		1. implementing INotifyCompositionCompletion interface
			//		2. Marking the method with [OnCompositionComplete] attribute
			//
			// There can be multiple methods specified with above criteria.
			//
			// The INotifyCompositionCompletion interface implementation
			// should be called after all of the attributed methods.

			MethodInfo interfaceMethod = null;

			if (typeof(INotifyCompositionCompletion).IsAssignableFrom(targetType))
			{
				interfaceMethod = targetType.GetInterfaceMap(typeof(INotifyCompositionCompletion)).TargetMethods[0];
			}

			// If the implementation of INotifyCompositionCompletion method also
			// has the [OnCompositionComplete] attribute set, it should not be
			// added twice to the list. So, we should filter it out from the
			// list of methods below:

			result.AddRange(
				targetType.GetMethods()
					.Where(IsOnCompositionCompleteMethod)
					.Where(method => method != interfaceMethod));

			// Add the INotifyCompositionCompletion interface after all of the
			// other methods, if any.

			if (interfaceMethod != null)
				result.Add(interfaceMethod);

			return result;
		}

		internal static void ApplyInitializationPoint(object targetObject, string memberName, MemberTypes memberType, object value)
		{
			var targetType = targetObject.GetType();

			if (memberType == MemberTypes.Field)
			{
				var fieldInfo = targetType.GetField(memberName);

				if (fieldInfo == null)
					throw new CompositionException(
					        $"Field '{memberName}' not found in type '{targetType.FullName}'.");

				fieldInfo.SetValue(targetObject, value);
				return;
			}

			if (memberType == MemberTypes.Property)
			{
				var propertyInfo = targetType.GetProperty(memberName);

				if (propertyInfo == null)
					throw new CompositionException(
					        $"Property '{memberName}' not found in type '{targetType.FullName}'.");

				propertyInfo.SetValue(targetObject, value, null);
				return;
			}

			if (memberType == MemberTypes.All)
			{
				var memberInfos = targetType.GetMember(memberName);

				if (memberInfos.Length == 0)
					throw new CompositionException(
					        $"Member '{memberName}' not found in type '{targetType.FullName}'.");

				var applicableMembers =
					memberInfos.Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property).ToArray();

				if (applicableMembers.Length == 0)
					throw new CompositionException(
					        $"No property / field found with name '{memberName}' in type '{targetType.FullName}'.");

				var memberInfo = applicableMembers[0];

				if (memberInfo.MemberType == MemberTypes.Field)
					((FieldInfo)memberInfo).SetValue(targetObject, value);
				else
					((PropertyInfo)memberInfo).SetValue(targetObject, value, null);

				return;
			}

			throw new ArgumentException("Specified member type is not supported: " + memberType);
		}

	    internal static ILocalComponentFactory CreateLocalFactory(Type component)
	    {
	        ILocalComponentFactory result;
	        if ((component.IsGenericType) && (component.ContainsGenericParameters))
	            result = new GenericLocalComponentFactory(component);
	        else
	            result = new LocalComponentFactory(component);

	        return result;
	    }

    }
}