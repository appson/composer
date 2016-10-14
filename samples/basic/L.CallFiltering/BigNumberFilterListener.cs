using System;
using System.Collections.Generic;
using Compositional.Composer.Emitter;
using Compositional.Composer.Interceptor;
using Compositional.Composer.Utility;

namespace Compositional.Composer.Samples.Basic
{
	class BigNumberFilterListener : ICompositionListener
	{
		#region Plugs

		[ComponentPlug]
		public IClassEmitter ClassEmitter { get; set; }

		#endregion

		#region Implementation of ICompositionListener

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			Console.WriteLine("LISTENER     - BigNumberFilterListener.OnComponentCreated({0})", identity == null ? "<null>" : identity.Type.Name);
			Console.WriteLine("             - Wrapping component for filtering big numbers.");

			if ((identity != null) && (identity.Type.IsInterface))
			{
				var handler = new InterceptingAdapterEmittedTypeHanlder(componentInstance, new BigNumberFilterInterceptor());
				var wrappedComponent = ClassEmitter.EmitInterfaceInstance(handler, identity.Type);

				componentInstance = wrappedComponent;
			}
		}

		public void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints, IEnumerable<object> initializationPointResults, Type componentTargetType, object componentInstance, object originalInstance)
		{
			// Do nothing
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			// Do nothing
		}

		#endregion
	}
}
