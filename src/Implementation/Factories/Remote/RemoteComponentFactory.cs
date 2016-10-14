using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Compositional.Composer.Factories.Remote
{
	public class RemoteComponentFactory : IComponentFactory
	{
		#region Fields

		private EndpointAddress _address;
		private Binding _binding;
		private Type _contractType;
		private List<Type> _knownTypes;

		#endregion

		#region Properties

		public EndpointAddress Address
		{
			get { return _address; }
			set { _address = value; }
		}

		public Binding Binding
		{
			get { return _binding; }
			set { _binding = value; }
		}

		public Type ContractType
		{
			get { return _contractType; }
			set { _contractType = value; }
		}

		public List<Type> KnownTypes
		{
			get { return _knownTypes; }
			set { _knownTypes = value; }
		}

		#endregion

		#region Constructors

		public RemoteComponentFactory()
		{
		}

		public RemoteComponentFactory(EndpointAddress address, Binding binding, Type contractType)
		{
			_address = address;
			_binding = binding;
			_contractType = contractType;
		}

		#endregion

		#region IComponentFactory Members

		public void Initialize(IComposer composer)
		{
		}

		public object Clone()
		{
			return CloneComponentFactory();
		}

		public IComponentFactory CloneComponentFactory()
		{
			return new RemoteComponentFactory
					{
						_address = _address,
						_binding = _binding,
						_contractType = _contractType,
						_knownTypes = _knownTypes
					};
		}

		public IEnumerable<Type> GetContractTypes()
		{
			return new List<Type>(new[] { _contractType });
		}

		public bool SharedAmongContracts
		{
			get { return true; }
		}

		public object GetComponentInstance(ContractIdentity contract,
										   IEnumerable<ICompositionListener> listenerChain)
		{
			var channelFactoryType =
				Type.GetType(
					string.Format(
						"System.ServiceModel.ChannelFactory`1[[{0}]], System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
						contract.Type.AssemblyQualifiedName));

			var channelFactory = channelFactoryType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

			var propertyInfo = channelFactoryType.GetProperty("Endpoint");

			var endpoint = propertyInfo.GetValue(channelFactory, null);

			endpoint.GetType().GetProperty("Binding").SetValue(endpoint, _binding, null);

			var result =
				channelFactoryType.GetMethod("CreateChannel", new[] { typeof(EndpointAddress) }).Invoke(channelFactory,
																									   new object[] { _address });


			if (_knownTypes != null)
			{
				foreach (var od in ((ServiceEndpoint)endpoint).Contract.Operations)
					if (od.Behaviors.Contains(typeof(CustomOperationBehavior)))
						od.Behaviors.Find<CustomOperationBehavior>().KnownTypes = _knownTypes;
			}

			var originalComponentObject = result;

			foreach (var compositionListener in listenerChain)
			{
				compositionListener.OnComponentCreated(contract, this, contract.Type, ref result, originalComponentObject);
			}

			return result;
		}

		#endregion
	}
}