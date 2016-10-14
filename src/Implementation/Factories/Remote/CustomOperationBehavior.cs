using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Compositional.Composer.Factories.Remote
{
	public class CustomOperationBehavior : Attribute, IOperationBehavior
	{
		public List<Type> KnownTypes
		{
			get { return _curBehavior.KnownTypes; }
			set { _curBehavior.KnownTypes = value; }
		}

		private CustomDataContractSerializerOperationBehavior _curBehavior;

		public void AddBindingParameters(OperationDescription description,
		                                 BindingParameterCollection parameters)
		{
		}

		public void ApplyClientBehavior(OperationDescription description,
		                                ClientOperation proxy)
		{
			_curBehavior = new CustomDataContractSerializerOperationBehavior(description);
			IOperationBehavior innerBehavior = _curBehavior;

			innerBehavior.ApplyClientBehavior(description, proxy);
		}

		public void ApplyDispatchBehavior(OperationDescription description,
		                                  DispatchOperation dispatch)
		{
			_curBehavior = new CustomDataContractSerializerOperationBehavior(description);
			IOperationBehavior innerBehavior = _curBehavior;

			innerBehavior.ApplyDispatchBehavior(description, dispatch);
		}

		public void Validate(OperationDescription description)
		{
		}
	}
}