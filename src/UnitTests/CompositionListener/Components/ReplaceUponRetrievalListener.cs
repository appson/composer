using System;
using System.Collections.Generic;

namespace Appson.Composer.UnitTests.CompositionListener.Components
{
	internal class ReplaceUponRetrievalListener : ICompositionListener
	{
		#region Implementation of ICompositionListener

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
		}

		public void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints, IEnumerable<object> initializationPointResults, Type componentTargetType, object componentInstance, object originalInstance)
		{
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			if (componentInstance is SampleComponentOne)
				componentInstance = new SampleComponentTwo();
		}

		#endregion
	}
}
