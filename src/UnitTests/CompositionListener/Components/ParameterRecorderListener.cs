using System;
using System.Collections.Generic;

namespace Appson.Composer.UnitTests.CompositionListener.Components
{
	internal class ParameterRecorderListener : ICompositionListener
	{
		public ContractIdentity CreatedIdentity;
		public IComponentFactory CreatedComponentFactory;
		public Type CreatedComponentTargetType;
		public object CreatedComponentInstance;
		public object CreatedOriginalInstance;

		public ContractIdentity ComposedIdentity;
		public IEnumerable<InitializationPointSpecification> ComposedInitializationPoints;
		public IEnumerable<object> ComposedInitializationPointResults;
		public Type ComposedComponentTargetType;
		public object ComposedComponentInstance;
		public object ComposedOriginalInstance;

		public ContractIdentity RetrievedIdentity;
		public IComponentFactory RetrievedComponentFactory;
		public Type RetrievedComponentTargetType;
		public object RetrievedComponentInstance;
		public object RetrievedOriginalInstance;

		#region Implementation of ICompositionListener

		public void OnComponentCreated(ContractIdentity identity, IComponentFactory componentFactory, 
			Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			CreatedIdentity = identity;
			CreatedComponentFactory = componentFactory;
			CreatedComponentTargetType = componentTargetType;
			CreatedComponentInstance = componentInstance;
			CreatedOriginalInstance = originalInstance;
		}

		public void OnComponentComposed(ContractIdentity identity, 
			IEnumerable<InitializationPointSpecification> initializationPoints, IEnumerable<object> initializationPointResults, 
			Type componentTargetType, object componentInstance, object originalInstance)
		{
			ComposedIdentity = identity;
			ComposedInitializationPoints = initializationPoints;
			ComposedInitializationPointResults = initializationPointResults;
			ComposedComponentTargetType = componentTargetType;
			ComposedComponentInstance = componentInstance;
			ComposedOriginalInstance = originalInstance;
		}

		public void OnComponentRetrieved(ContractIdentity identity, IComponentFactory componentFactory, 
			Type componentTargetType, ref object componentInstance, object originalInstance)
		{
			RetrievedIdentity = identity;
			RetrievedComponentFactory = componentFactory;
			RetrievedComponentTargetType = componentTargetType;
			RetrievedComponentInstance = componentInstance;
			RetrievedOriginalInstance = originalInstance;
		}

		#endregion
	}
}
