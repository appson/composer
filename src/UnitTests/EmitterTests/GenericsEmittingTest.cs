using Compositional.Composer.UnitTests.EmitterTests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.EmitterTests
{
	[TestClass]
	public class GenericsEmittingTest
	{
		public TestContext TestContext { get; set; }
		private TestingEmittedTypeHandler _dth;

		#region Additional test attributes

		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_dth = new TestingEmittedTypeHandler();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

	}
//* Interface with type parameter (generic) - empty
//* Interface with type parameter (generic) - T appears in method return value
//* Interface with type parameter (generic) - T appears in method parameter
//* Interface with type parameter (generic) - T appears as a property value
//* Void method with generic reference-type argument
//* Void method with generic value-type argument
//* Void method with generic reference-type "ref" argument
//* Void method with generic value-type "ref" argument
//* Method with type parameters (generic method), without arguments
//* Method with type parameters (generic method), with T argument
//* Generic Event
}
