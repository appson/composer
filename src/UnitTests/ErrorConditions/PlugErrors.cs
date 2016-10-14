using Appson.Composer.UnitTests.ErrorConditions.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ErrorConditions
{
	[TestClass]
	public class PlugErrors
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
		public void PlugWithoutSetter()
		{
			_context.Register(typeof(PlugWithoutSetter));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PlugWithPrivateSetter()
		{
			_context.Register(typeof(PlugWithPrivateSetter));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void PlugWithNonContractType()
		{
			_context.Register(typeof(NonContractPlugType));
		}
	}
}
