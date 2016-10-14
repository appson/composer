using System;

namespace Compositional.Composer.UnitTests.InterceptorTests.Components
{
	internal class SomeInterfaceImplementation : ISomeInterface
	{
		public string SomeMethodS;
		public int SomeMethodI;

		#region Implementation of ISomeInterface

		public string SomeMethod(string s, int i)
		{
			SomeMethodS = s;
			SomeMethodI = i;

			return "SomeValue";
		}

		public void ThrowException()
		{
			throw new NullReferenceException();
		}

		#endregion
	}
}
