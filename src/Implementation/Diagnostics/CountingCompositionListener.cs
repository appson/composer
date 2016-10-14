using System;
using System.Collections.Generic;
using Appson.Composer.Utility.Matching;

namespace Appson.Composer.Diagnostics
{
	public class CountingCompositionListener : ICompositionListener
	{
		#region Static fields and constructor

		public static List<CountingCompositionListener> Instances { get; private set; }

		static CountingCompositionListener()
		{
			Instances = new List<CountingCompositionListener>();
		}

		#endregion
		
		public int OnComponentCreatedCount { get; private set; }
		public int OnComponentComposedCount { get; private set; }
		public int OnComponentRetrievedCount { get; private set; }

		public ITypeFilter IncludedContracts { get; set; }
		public ITypeFilter IncludedComponents { get; set; }
		public string Title { get; set; }

		public CountingCompositionListener()
		{
			OnComponentCreatedCount = 0;
			OnComponentComposedCount = 0;
			OnComponentRetrievedCount = 0;
		}

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			if (IncludedContracts != null)
			{
				if (!IncludedContracts.Match(identity.Type))
					return;
			}

			if (IncludedComponents != null)
			{
				if (!IncludedComponents.Match(componentTargetType))
					return;
			}

			OnComponentCreatedCount++;
		}

		public void OnComponentComposed(ContractIdentity identity, IEnumerable<InitializationPointSpecification> initializationPoints, IEnumerable<object> initializationPointResults, Type componentTargetType, object componentInstance, object originalInstance)
		{
			if (IncludedContracts != null)
			{
				if (!IncludedContracts.Match(identity.Type))
					return;
			}

			if (IncludedComponents != null)
			{
				if (!IncludedComponents.Match(componentTargetType))
					return;
			}

			OnComponentComposedCount++;
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory, Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			if (IncludedContracts != null)
			{
				if (!IncludedContracts.Match(identity.Type))
					return;
			}

			if (IncludedComponents != null)
			{
				if (!IncludedComponents.Match(componentTargetType))
					return;
			}

			OnComponentRetrievedCount++;
		}
	}
}
