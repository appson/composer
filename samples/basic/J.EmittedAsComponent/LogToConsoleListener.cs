using System;
using System.Collections.Generic;

namespace Compositional.Composer.Samples.Basic
{
	public class LogToConsoleListener : ICompositionListener
	{
		#region Implementation of ICompositionListener

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			Console.WriteLine("LISTENER     - OnComponentCreated: {0}", identity.Type.Name);
		}

		public void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints, IEnumerable<object> initializationPointResults, Type componentTargetType, object componentInstance, object originalInstance)
		{
			Console.WriteLine("LISTENER     - OnComponentComposed: {0}", identity.Type.Name);
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			Console.WriteLine("LISTENER     - OnComponentRetrieved: {0}", identity.Type.Name);
		}

		#endregion
	}
}
