using System;
using System.Collections.Generic;
using System.Linq;

namespace Appson.Composer.Factories
{
	public class GenericLocalComponentFactory : IComponentFactory
	{
		private IComposer _composer;

		/// <summary>
		/// Generic type definition (obtained by calling ) 
		/// -> 
		/// Constructed generic type (obtained from base classes and interfaces)
		/// </summary>
		private readonly IDictionary<Type, Type> _contractTypes;

		private readonly IDictionary<Type, LocalComponentFactory> _subFactories; 

		private readonly Type _targetType;
		private readonly List<InitializationPointSpecification> _initializationPoints;

		public GenericLocalComponentFactory(Type targetType)
		{
			if ((!targetType.ContainsGenericParameters) || (!targetType.IsGenericType))
				throw new ArgumentException("TargetType in GenericLocalComponentFactory should be an open generic type.");

			_targetType = targetType;
			_composer = null;

			_contractTypes = new Dictionary<Type, Type>();
			_subFactories = new Dictionary<Type, LocalComponentFactory>();
			_initializationPoints = new List<InitializationPointSpecification>();

			ExtractContractTypes();
		}

		#region Implementation of IComponentFactory

		public void Initialize(IComposer composer)
		{
			_composer = composer;
		}

		public IEnumerable<Type> GetContractTypes()
		{
			return _contractTypes.Keys;
		}

		public object GetComponentInstance(ContractIdentity contract, IEnumerable<ICompositionListener> listenerChain)
		{
			var requestedClosedContractType = contract.Type;

			if (!requestedClosedContractType.IsGenericType)
				throw new CompositionException("Requested contract " + requestedClosedContractType.Name + " is not a generic type.");

			if (requestedClosedContractType.ContainsGenericParameters)
				throw new CompositionException("Requested contract " + requestedClosedContractType.Name +
				                               " is not fully constructed and closed.");

			var requestedGenericContractType = contract.Type.GetGenericTypeDefinition();
			if (!_contractTypes.ContainsKey(requestedGenericContractType))
				throw new CompositionException("The requested generic contract type definition " +
				                               requestedGenericContractType.Name +
				                               " is not among the supported contracts for this factory.");

			var originalGenericContractType = _contractTypes[requestedGenericContractType];
			var closedTargetType = CloseGenericType(_targetType, originalGenericContractType, requestedClosedContractType);

			if (!_subFactories.ContainsKey(closedTargetType))
			{
				_subFactories[closedTargetType] = new LocalComponentFactory(closedTargetType);
				_subFactories[closedTargetType].InitializationPoints.AddRange(_initializationPoints);
				_subFactories[closedTargetType].Initialize(_composer);
			}

			return _subFactories[closedTargetType].GetComponentInstance(contract, listenerChain);
		}

		#endregion

		#region Public Properties

		public List<InitializationPointSpecification> InitializationPoints
		{
			get
			{
				if (_composer != null)
					throw new InvalidOperationException("Cannot access InitializationPoints when the factory is initialized.");

				return _initializationPoints;
			}
		}

		#endregion

		#region Private helper methods

		private void ExtractContractTypes()
		{
			var openContracts = ComponentContextUtils.FindContracts(_targetType)
				.Where(t => t.ContainsGenericParameters && t.IsGenericType);

			foreach (var openContract in openContracts)
			{
				_contractTypes.Add(openContract.GetGenericTypeDefinition(), openContract);
			}

			if (_contractTypes.Count < 1)
				throw new CompositionException("No open contracts found on the type " + _targetType.Name);
		}

		private Type CloseGenericType(Type openType, Type templateType, Type closedType)
		{
			Type[] templateTypeParams = templateType.GetGenericArguments();
			Type[] closedTypeParams = closedType.GetGenericArguments();

			var currentType = openType;

			while (currentType.ContainsGenericParameters)
			{
				Type[] currentTypeParams = currentType.GetGenericArguments();

				int currentTypeParamIndex = -1;

				for (int i = 0; i < currentTypeParams.Length; i++)
					if (currentTypeParams[i].IsGenericParameter)
						currentTypeParamIndex = i;

				if (currentTypeParamIndex < 0)
					return null;

				Type currentTypeParam = currentTypeParams[currentTypeParamIndex];

				int closedTypeParamIndex = -1;
				for (int i = 0; i < templateTypeParams.Length; i++)
					if (templateTypeParams[i] == currentTypeParam)
						closedTypeParamIndex = i;

				if (closedTypeParamIndex < 0)
					return null;

				Type closedTypeParam = closedTypeParams[closedTypeParamIndex];

				currentTypeParams[currentTypeParamIndex] = closedTypeParam;

				currentType = currentType
					.GetGenericTypeDefinition()
					.MakeGenericType(currentTypeParams);
			}

			return currentType;
		}

		#endregion
	}
}