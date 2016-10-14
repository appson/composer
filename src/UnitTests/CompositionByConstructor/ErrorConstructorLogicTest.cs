using Compositional.Composer.UnitTests.CompositionByConstructor.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.CompositionByConstructor
{
	[TestClass]
	public class ErrorConstructorLogicTest
	{
		public TestContext TestContext { get; set; }
		private ComponentContext _context;

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
			_context = new ComponentContext();
			_context.Register(typeof(SampleComponentA));
			_context.Register(typeof(SampleComponentB));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PrivateDefaultConstructorThrows()
		{
			_context.Register(typeof(PrivateDefaultSingleConstructor));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PrivateDefaultWithOtherUnmarkedConstructorsThrows()
		{
			_context.Register(typeof(PrivateDefaultNoneMarkedFromOtherConstructors));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PrivateMarkedConstructorThrows()
		{
			_context.Register(typeof(PrivateMarkedConstructor));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void MultipleMarkedConstructorsThrows()
		{
			_context.Register(typeof(MultipleMarkedConstructors));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void MultipleNonDefaultNoneMarkedThrows()
		{
			_context.Register(typeof(NoDefaultMultipleUnmarkedConstructors));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ConstructorParameterAreRequired()
		{
			_context.UnregisterFamily(typeof(ISampleContractA));
			_context.Register(typeof(MultiConstructorsWithDefaultAndMarked));

			_context.GetComponent<MultiConstructorsWithDefaultAndMarked>();
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void NonContractParameterThrows()
		{
			_context.Register(typeof(ConstructorWithIntegerParam));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ExtraNamesThrows()
		{
			_context.Register(typeof(NamesMoreThanParamCount));
		}


	}
}
