using System;
using System.Collections.Generic;

namespace Compositional.Composer.Samples.Basic
{
	class CalculatorFactory : IComponentFactory
	{
		private IComposer _composer;

		#region Implementation of ICloneable

		public object Clone()
		{
			return CloneComponentFactory();
		}

		#endregion

		#region Implementation of IComponentFactory

		public void Initialize(IComposer composer)
		{
			_composer = composer;
		}

		public IComponentFactory CloneComponentFactory()
		{
			return new CalculatorFactory();
		}

		public IEnumerable<Type> GetContracts()
		{
			return new[] {typeof (ICalculator)};
		}

		public object GetComponentInstance(ContractIdentity contract, IEnumerable<ICompositionListener> listenerChain)
		{
			var result = new DefaultCalculator();

			result.Adder = _composer.GetComponent<IAdder>();
			result.Multiplier = _composer.GetComponent<IMultiplier>();
			result.Divider = _composer.GetComponent<IDivider>();

			return result;
		}

		public bool SharedAmongContracts
		{
			get { return false; }
		}

		#endregion
	}
}
