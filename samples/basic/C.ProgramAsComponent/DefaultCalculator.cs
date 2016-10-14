using System;

namespace Compositional.Composer.Samples.Basic
{
    /// <summary>
    /// The component providing the ICalculator contract.
    /// This component uses other components, via required
    /// contracts, to perform the calculations.
    /// </summary>
    [Component]
	public class DefaultCalculator : ICalculator
	{
		#region Plugs

        // The following fields are backing fields for required
        // contracts.

        private IAdder _adder;
		private IMultiplier _multiplier;
		private IDivider _divider;

        // This [ComponentPlug] attribute tells Composer
        // to fill the property with an appropriate component
        // instance, that is able to provide the IAdder contract.

        [ComponentPlug]
		public IAdder Adder
		{
			get
			{
				return _adder;
			}
			set
			{
				_adder = value;
				Console.WriteLine("SET PLUG     - DefaultCalculator.Adder");
			}
		}

		[ComponentPlug]
		public IMultiplier Multiplier
		{
			get
			{
				return _multiplier;
			}
			set
			{
				_multiplier = value;
				Console.WriteLine("SET PLUG     - DefaultCalculator.Multiplier");
			}
		}

		[ComponentPlug]
		public IDivider Divider
		{
			get
			{
				return _divider;
			}
			set
			{
				_divider = value;
				Console.WriteLine("SET PLUG     - DefaultCalculator.Divider");
			}
		}

		#endregion

		#region Initialization

		public DefaultCalculator()
		{
			Console.WriteLine("CONSTRUCTOR  - DefaultCalculator");
		}

		#endregion

		#region Implementation of ICalculator

		public int Add(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultCalculator.Add({0}, {1})", a, b);

			return Adder.Add(a, b);
		}

		public int Subtract(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultCalculator.Subtract({0}, {1})", a, b);

			return Adder.Add(a, -b);
		}

		public int Multiply(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultCalculator.Multiply({0}, {1})", a, b);

			return Multiplier.Multiply(a, b);
		}

		public int Divide(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultCalculator.Divide({0}, {1})", a, b);

			return Divider.Divide(a, b);
		}

		public int Remainder(int a, int b)
		{
			Console.WriteLine("METHOD CALL  - DefaultCalculator.Remainder({0}, {1})", a, b);

			return Divider.Remainder(a, b);
		}

		#endregion
	}
}
