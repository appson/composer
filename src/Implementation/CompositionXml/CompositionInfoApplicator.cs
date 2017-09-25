using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Xml;
using Appson.Composer.CompositionalQueries;
using Appson.Composer.CompositionXml.Info;
using Appson.Composer.CompositionXml.ValueParser;
using Appson.Composer.Factories;
using Appson.Composer.Factories.Remote;
using Appson.Composer.Utility;

namespace Appson.Composer.CompositionXml
{
	internal static class CompositionInfoApplicator
	{
		#region Initialization

		private static readonly Dictionary<Type, CommandRunner> Runners;

		static CompositionInfoApplicator()
		{
			Runners = new Dictionary<Type, CommandRunner>
			          	{
			          		{typeof (UsingInfo), RunUsing},
			          		{typeof (UsingAssemblyInfo), RunUsingAssembly},
			          		{typeof (IncludeInfo), RunInclude},
			          		{typeof (SetVariableInfo), RunSetVariable},
			          		{typeof (RemoveVariableInfo), RunRemoveVariable},
			          		{typeof (RegisterCompositionListenerInfo), RunRegisterCompositionListener},
			          		{typeof (UnregisterCompositionListenerInfo), RunUnregisterCompositionListener},
			          		{typeof (RegisterAssemblyInfo), RunRegisterAssembly},
			          		{typeof (RegisterComponentInfo), RunRegisterComponent},
			          		{typeof (RemoteComponentInfo), RunRemoteComponent},
			          		{typeof (UnregisterInfo), RunUnregister},
			          		{typeof (UnregisterFamilyInfo), RunUnregisterFamily}
			          	};
		}

		#endregion

		#region Public methods

		public static void ApplyCompositionInfo(CompositionInfo info, XmlProcessingContext xmlProcessingContext)
		{
			if (info.CommandInfos == null)
				return;

			foreach (var commandInfo in info.CommandInfos)
			{
				if (!Runners.ContainsKey(commandInfo.GetType()))
					throw new CompositionException("Provided type is not supported for applying to a component context: " +
					                               commandInfo.GetType().FullName);

				var runner = Runners[commandInfo.GetType()];


				try
				{
					xmlProcessingContext.EnterRunningLocation("Processing '" + commandInfo + "'");
					runner(commandInfo, xmlProcessingContext);
					xmlProcessingContext.LeaveRunningLocation();
				}
				catch (Exception e)
				{
					// Ignore the exception if the "IgnoreOnError" property is set.
					// If not, just report the exception (in case it happened in an
					// include command) and let it bubble up.

					if (!commandInfo.IgnoreOnError)
					{
						// TODO: Fix logging / tracing mechanism

						xmlProcessingContext.ReportError("Exception: " + e.Message);
						throw;
					}

					// If exception has occured, then the "LeaveRunningLocation" 
					// method is not executed in the "try" block, so call it here.

					xmlProcessingContext.LeaveRunningLocation();
				}
			}
		}

		#endregion

		#region Runners: Preprocessors (Using, UsingAssembly, Include)

		private static void RunUsing(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is UsingInfo usingInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.TypeCache.NamespaceUsings.Add(usingInfo.Namespace);
		}

		private static void RunUsingAssembly(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is UsingAssemblyInfo usingAssemblyInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var assembly = Assembly.Load(usingAssemblyInfo.FullName);
			xmlProcessingContext.TypeCache.CacheAssembly(assembly);
		}

		private static void RunInclude(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is IncludeInfo includeInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			if (includeInfo.Path != null)
			{
				ComposerXmlUtil.ProcessCompositionXml(includeInfo.Path, xmlProcessingContext);
			}
			else
			{
				var assembly = Assembly.Load(includeInfo.AssemblyName);
				ComposerXmlUtil.ProcessCompositionXmlFromResource(assembly, includeInfo.ManifestResourceName, xmlProcessingContext);
			}
		}

		#endregion

		#region Runners: variable operations

		private static void RunSetVariable(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is SetVariableInfo setVariableInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var value = CreateLazyXmlValue(setVariableInfo.XElements, setVariableInfo.XAttributes, xmlProcessingContext);
			xmlProcessingContext.ComponentContext.SetVariable(setVariableInfo.Name, value);
		}

		private static void RunRemoveVariable(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is RemoveVariableInfo removeVariableInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.ComponentContext.RemoveVariable(removeVariableInfo.Name);
		}

		#endregion

		#region Runners: Composition listeners

		private static void RunRegisterCompositionListener(CompositionCommandInfo info,
		                                                   XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is RegisterCompositionListenerInfo registerCompositionListenerInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var listener = XmlValueParser.ParseValue(registerCompositionListenerInfo.XElements,
			                                         registerCompositionListenerInfo.XAttributes,
			                                         xmlProcessingContext);

			if (listener == null)
			{
				xmlProcessingContext.ReportError("Provided value is null for registering composition listeners.");
				return;
			}

		    if (!(listener is ICompositionListener compositionListener))
			{
				xmlProcessingContext.ReportError(
					"Registering composition listeners are only allowed for ICompositionListener implementations. Provided type: " +
					listener.GetType().FullName);
				return;
			}

			xmlProcessingContext.ComponentContext.RegisterCompositionListener(registerCompositionListenerInfo.Name,
			                                                                  compositionListener);
		}

		private static void RunUnregisterCompositionListener(CompositionCommandInfo info,
		                                                     XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is UnregisterCompositionListenerInfo unregisterCompositionListenerInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.ComponentContext.UnregisterCompositionListener(unregisterCompositionListenerInfo.Name);
		}

		#endregion

		#region Runners: Component registration

		private static void RunRegisterAssembly(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is RegisterAssemblyInfo registerAssemblyInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var assembly = Assembly.Load(registerAssemblyInfo.FullName);

			 xmlProcessingContext.ComponentContext.RegisterAssembly(assembly);
		}

		private static void RunRegisterComponent(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is RegisterComponentInfo registerComponentInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			// Extract and load contract type

			Type contractType = null;

			if (registerComponentInfo.ContractType != null)
			{
				contractType = SimpleTypeParserUtil.ParseType(registerComponentInfo.ContractType, xmlProcessingContext);

				if (contractType == null)
				{
					xmlProcessingContext.ReportError(
					    $"Type '{registerComponentInfo.ContractType}' could not be loaded.");
					return;
				}
			}

			// Get contract name

			var contractName = registerComponentInfo.ContractName;

			// Build ComponentConfiguration

			var componentType = SimpleTypeParserUtil.ParseType(registerComponentInfo.Type, xmlProcessingContext);
			if (componentType == null)
			{
				xmlProcessingContext.ReportError($"Type '{registerComponentInfo.Type}' could not be loaded.");
				return;
			}
			
			IComponentFactory componentFactory;
			List<InitializationPointSpecification> initializationPoints;

			if ((componentType.IsGenericType) && (componentType.ContainsGenericParameters))
			{
				var genericLocalComponentFactory = new GenericLocalComponentFactory(componentType);

				componentFactory = genericLocalComponentFactory;
				initializationPoints = genericLocalComponentFactory.InitializationPoints;
			}
			else
			{
				var localComponentFactory = new LocalComponentFactory(componentType);

				componentFactory = localComponentFactory;
				initializationPoints = localComponentFactory.InitializationPoints;
			}

			// Add each configured plug, into the InitializationPoints
			// in the component configuration.

			foreach (var plugInfo in registerComponentInfo.Plugs)
			{
				xmlProcessingContext.EnterRunningLocation($"Plug '{plugInfo.Name}'");

				var plugRefType = SimpleTypeParserUtil.ParseType(plugInfo.RefType, xmlProcessingContext);
				if (plugRefType == null)
				{
					xmlProcessingContext.ReportError($"Type '{plugInfo.RefType}' could not be loaded.");
					xmlProcessingContext.LeaveRunningLocation();
					return;
				}

				var plugRefName = plugInfo.RefName;

				initializationPoints.Add(new InitializationPointSpecification(
				                                          	plugInfo.Name,
				                                          	MemberTypes.All,
				                                          	true,
				                                          	new ComponentQuery(plugRefType, plugRefName)));
				
				// TODO: Add support for optional plugs in Composition XML

				xmlProcessingContext.LeaveRunningLocation();
			}

			// Add each configuration point, into the InitializationPoints
			// in the component configuration.

			foreach (var configurationPointInfo in registerComponentInfo.ConfigurationPoints)
			{
				xmlProcessingContext.EnterRunningLocation($"ConfigurationPoint '{configurationPointInfo.Name}'");

				var value = CreateLazyXmlValue(configurationPointInfo.XElements,
				                               configurationPointInfo.XAttributes,
				                               xmlProcessingContext);

				initializationPoints.Add(new InitializationPointSpecification(
				                                          	configurationPointInfo.Name,
				                                          	MemberTypes.All,
				                                          	true,
				                                          	new LazyValueQuery(value)));

				xmlProcessingContext.LeaveRunningLocation();
			}

			// Register the component into the component context.

			if (contractType == null)
				xmlProcessingContext.ComponentContext.Register(contractName, componentFactory);
			else
				xmlProcessingContext.ComponentContext.Register(contractType, contractName, componentFactory);
		}

		private static void RunRemoteComponent(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is RemoteComponentInfo remoteComponentInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			// Extract and load contract type

			var contractType = SimpleTypeParserUtil.ParseType(remoteComponentInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError(
				    $"Type '{remoteComponentInfo.ContractType}' could not be loaded.");
				return;
			}

			// Get contract name

			var contractName = remoteComponentInfo.ContractName;

			// Create end point address

			var serverAddress = remoteComponentInfo.ServerAddress ??
			                    (string)
			                    xmlProcessingContext.ComponentContext.GetVariable(remoteComponentInfo.ServerAddressVariableName);

			var spnIdentity = remoteComponentInfo.SpnIdentity ??
			                  contractType.Name;

			var endpointAddress = new EndpointAddress(
				new Uri(serverAddress),
				EndpointIdentity.CreateSpnIdentity(spnIdentity));

			// Create binding

			var securityMode = (SecurityMode) Enum.Parse(
			                                  	typeof (SecurityMode),
			                                  	remoteComponentInfo.SecurityMode ?? "None",
			                                  	true);

			var binding = new NetTcpBinding(securityMode)
			              	{
			              		MaxBufferSize = (remoteComponentInfo.MaxBufferSizeNullable.HasValue
			              		                 	?
			              		                 		remoteComponentInfo.MaxBufferSize
			              		                 	:
			              		                 		16777216),
			              		MaxReceivedMessageSize = (remoteComponentInfo.MaxReceivedMessageSizeNullable.HasValue
			              		                          	?
			              		                          		remoteComponentInfo.MaxReceivedMessageSize
			              		                          	:
			              		                          		16777216)
			              	};

			// Extract list of known types

			List<Type> knownTypes = null;

			if (!string.IsNullOrEmpty(remoteComponentInfo.KnownTypesVariableName))
				knownTypes =
					xmlProcessingContext.ComponentContext.GetVariable(remoteComponentInfo.KnownTypesVariableName) as List<Type>;

			// Build ComponentConfiguration

			var componentFactory = new RemoteComponentFactory
			                       	{
			                       		Address = endpointAddress,
			                       		Binding = binding,
			                       		ContractType = contractType,
			                       		KnownTypes = knownTypes
			                       	};

			// Register the component into the component context.

			xmlProcessingContext.ComponentContext.Register(contractType, contractName, componentFactory);
		}

		private static void RunUnregister(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is UnregisterInfo unregisterInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var contractType = SimpleTypeParserUtil.ParseType(unregisterInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError($"Type '{unregisterInfo.ContractType}' could not be loaded.");
				return;
			}
			var contractIdentity = new ContractIdentity(contractType, unregisterInfo.ContractName);

			xmlProcessingContext.ComponentContext.Unregister(contractIdentity);
		}

		private static void RunUnregisterFamily(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
		    if (!(info is UnregisterFamilyInfo unregisterFamilyInfo))
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var contractType = SimpleTypeParserUtil.ParseType(unregisterFamilyInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError($"Type '{unregisterFamilyInfo.ContractType}' could not be loaded.");
				return;
			}

			xmlProcessingContext.ComponentContext.UnregisterFamily(contractType);
		}

		#endregion

		#region Nested type declarations

		private delegate void CommandRunner(CompositionCommandInfo commandInfo, XmlProcessingContext xmlProcessingContext);

		#endregion

		#region Private helper methods

		private static Lazy<object> CreateLazyXmlValue(XmlElement[] xElements, XmlAttribute[] xAttributes,
                                        XmlProcessingContext xmlProcessingContext)
		{
			return new Lazy<object>(delegate
			                        	{
			                        		object result = XmlValueParser.ParseValue(xElements, xAttributes, xmlProcessingContext);
											xmlProcessingContext.ThrowIfErrors();

			                        		xElements = null;
			                        		xAttributes = null;
			                        		xmlProcessingContext = null;

			                        		return result;
			                        	});
		}


		#endregion
	}
}