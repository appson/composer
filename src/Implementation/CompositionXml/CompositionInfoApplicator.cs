using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Xml;
using Compositional.Composer.CompositionalQueries;
using Compositional.Composer.CompositionXml.Info;
using Compositional.Composer.CompositionXml.ValueParser;
using Compositional.Composer.Factories;
using Compositional.Composer.Factories.Remote;
using Compositional.Composer.Utility;

namespace Compositional.Composer.CompositionXml
{
	internal static class CompositionInfoApplicator
	{
		#region Initialization

		private static readonly Dictionary<Type, CommandRunner> runners;

		static CompositionInfoApplicator()
		{
			runners = new Dictionary<Type, CommandRunner>
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
				if (!runners.ContainsKey(commandInfo.GetType()))
					throw new CompositionException("Provided type is not supported for applying to a component context: " +
					                               commandInfo.GetType().FullName);

				var runner = runners[commandInfo.GetType()];


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
			var usingInfo = info as UsingInfo;

			if (usingInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.TypeCache.NamespaceUsings.Add(usingInfo.Namespace);
		}

		private static void RunUsingAssembly(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var usingAssemblyInfo = info as UsingAssemblyInfo;

			if (usingAssemblyInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var assembly = Assembly.Load(usingAssemblyInfo.FullName);
			xmlProcessingContext.TypeCache.CacheAssembly(assembly);
		}

		private static void RunInclude(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var includeInfo = info as IncludeInfo;

			if (includeInfo == null)
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
			var setVariableInfo = info as SetVariableInfo;

			if (setVariableInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var value = CreateLazyXmlValue(setVariableInfo.XElements, setVariableInfo.XAttributes, xmlProcessingContext);
			xmlProcessingContext.ComponentContext.SetVariable(setVariableInfo.Name, value);
		}

		private static void RunRemoveVariable(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var removeVariableInfo = info as RemoveVariableInfo;

			if (removeVariableInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.ComponentContext.RemoveVariable(removeVariableInfo.Name);
		}

		#endregion

		#region Runners: Composition listeners

		private static void RunRegisterCompositionListener(CompositionCommandInfo info,
		                                                   XmlProcessingContext xmlProcessingContext)
		{
			var registerCompositionListenerInfo = info as RegisterCompositionListenerInfo;

			if (registerCompositionListenerInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var listener = XmlValueParser.ParseValue(registerCompositionListenerInfo.XElements,
			                                         registerCompositionListenerInfo.XAttributes,
			                                         xmlProcessingContext);

			if (listener == null)
			{
				xmlProcessingContext.ReportError("Provided value is null for registering composition listeners.");
				return;
			}

			var compositionListener = listener as ICompositionListener;

			if (compositionListener == null)
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
			var unregisterCompositionListenerInfo = info as UnregisterCompositionListenerInfo;

			if (unregisterCompositionListenerInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			xmlProcessingContext.ComponentContext.UnregisterCompositionListener(unregisterCompositionListenerInfo.Name);
		}

		#endregion

		#region Runners: Component registration

		private static void RunRegisterAssembly(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var registerAssemblyInfo = info as RegisterAssemblyInfo;

			if (registerAssemblyInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var assembly = Assembly.Load(registerAssemblyInfo.FullName);

			 xmlProcessingContext.ComponentContext.RegisterAssembly(assembly);
		}

		private static void RunRegisterComponent(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var registerComponentInfo = info as RegisterComponentInfo;

			if (registerComponentInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			// Extract and load contract type

			Type contractType = null;

			if (registerComponentInfo.ContractType != null)
			{
				contractType = SimpleTypeParserUtil.ParseType(registerComponentInfo.ContractType, xmlProcessingContext);

				if (contractType == null)
				{
					xmlProcessingContext.ReportError(
						string.Format("Type '{0}' could not be loaded.", registerComponentInfo.ContractType));
					return;
				}
			}

			// Get contract name

			var contractName = registerComponentInfo.ContractName;

			// Build ComponentConfiguration

			var componentType = SimpleTypeParserUtil.ParseType(registerComponentInfo.Type, xmlProcessingContext);
			if (componentType == null)
			{
				xmlProcessingContext.ReportError(string.Format("Type '{0}' could not be loaded.", registerComponentInfo.Type));
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
				xmlProcessingContext.EnterRunningLocation(string.Format("Plug '{0}'", plugInfo.Name));

				var plugRefType = SimpleTypeParserUtil.ParseType(plugInfo.RefType, xmlProcessingContext);
				if (plugRefType == null)
				{
					xmlProcessingContext.ReportError(string.Format("Type '{0}' could not be loaded.", plugInfo.RefType));
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
				xmlProcessingContext.EnterRunningLocation(string.Format("ConfigurationPoint '{0}'", configurationPointInfo.Name));

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
			var remoteComponentInfo = info as RemoteComponentInfo;

			if (remoteComponentInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			// Extract and load contract type

			var contractType = SimpleTypeParserUtil.ParseType(remoteComponentInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError(
					string.Format("Type '{0}' could not be loaded.", remoteComponentInfo.ContractType));
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
			var unregisterInfo = info as UnregisterInfo;

			if (unregisterInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var contractType = SimpleTypeParserUtil.ParseType(unregisterInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError(string.Format("Type '{0}' could not be loaded.", unregisterInfo.ContractType));
				return;
			}
			var contractIdentity = new ContractIdentity(contractType, unregisterInfo.ContractName);

			xmlProcessingContext.ComponentContext.Unregister(contractIdentity);
		}

		private static void RunUnregisterFamily(CompositionCommandInfo info, XmlProcessingContext xmlProcessingContext)
		{
			var unregisterFamilyInfo = info as UnregisterFamilyInfo;

			if (unregisterFamilyInfo == null)
				throw new ArgumentException("Invalid runner input type: error in static setup.");

			var contractType = SimpleTypeParserUtil.ParseType(unregisterFamilyInfo.ContractType, xmlProcessingContext);

			if (contractType == null)
			{
				xmlProcessingContext.ReportError(string.Format("Type '{0}' could not be loaded.", unregisterFamilyInfo.ContractType));
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