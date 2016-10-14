using System;
using System.Collections.Generic;
using Appson.Composer.Emitter;
using Appson.Composer.Interceptor;
using Appson.Composer.Utility.Matching;
using Appson.Composer.Utility;


namespace Appson.Composer.Diagnostics
{
	public class TracingCompositionListener : ICompositionListener
	{
		public TracingCompositionListener()
		{
			IncludedContracts = null;

			TraceExceptions = true;
			TraceReturnValues = false;
			TraceArgumentCount = true;
			TraceArgumentTypes = false;
			UseFullTypeNames = false;
		}

		[ComponentPlug]
		public IClassEmitter ClassEmitter { get; set; }

		public ITypeFilter IncludedContracts { get; set; }

		public bool TraceExceptions { get; set; }
		public bool TraceReturnValues { get; set; }
		public bool TraceArgumentCount { get; set; }
		public bool TraceArgumentTypes { get; set; }
		public bool UseFullTypeNames { get; set; }

		#region ICompositionListener Members

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType,
		                               ref object componentInstance, object originalInstance)
		{
			var match = false;

			if (IncludedContracts != null)
				match |= IncludedContracts.Match(identity.Type);

			if (!match)
				return;

			if (!identity.Type.IsInterface)
				throw new ArgumentException(
					"Can not create wrapper for non-interface type contracts. " +
					"The transaction composition listener should be configured such that " +
					"the non-interface type contracts do not match the wrapping criteria.");


			var interceptor = new TracingInterceptor(identity.Type.Name);
			var callHanlder = new InterceptingAdapterEmittedTypeHanlder(componentInstance, interceptor);

			componentInstance = ClassEmitter.EmitInterfaceInstance(callHanlder, identity.Type);
		}

		public void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints,
		                                IEnumerable<object> initializationPointResults,
		                                Type componentTargetType, object componentInstance, object originalInstance)
		{
			// Ignore.
			// There's nothing to do after a component is composed.
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory,
		                                 Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			// Ignore.
			// There's nothing to do upon retrieval of a component.
		}

		#endregion
	}
}