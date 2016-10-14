using Appson.Composer.UnitTests.ErrorConditions.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ErrorConditions
{
	[TestClass]
	public class ComponentErrors
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
		public void RegisterTypeThatIsNotComponent()
		{
			_context.Register(typeof(NotAComponent));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void RegisterComponentWithoutProvidedContracts()
		{
			_context.Register(typeof(NoProvidedContract));
		}
	}
}
