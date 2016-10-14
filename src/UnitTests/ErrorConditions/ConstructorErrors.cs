using Appson.Composer.UnitTests.ErrorConditions.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ErrorConditions
{
	[TestClass]
	public class ConstructorErrors
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
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void MultipleCompConstructors()
		{
			_context.Register(typeof(MultipleCompositionConstructors));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void NoCompConstructors()
		{
			_context.Register(typeof(NoCompositionContructors));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PrivateCompConstructor()
		{
			_context.Register(typeof(PrivateCompositionConstructor));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void NonContractParam()
		{
			_context.Register(typeof(NonContractConstructorArg));
		}
	}
}
