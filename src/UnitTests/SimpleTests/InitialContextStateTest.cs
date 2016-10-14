using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.SimpleTests
{
	[TestClass]
	public class InitialContextStateTest
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
		public void IComposerIsRegistered()
		{
			var c = _context.GetComponent<IComposer>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void IComponentContextIsRegistered()
		{
			var c = _context.GetComponent<IComponentContext>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void ComponentContextIsRegistered()
		{
			var c = _context.GetComponent<ComponentContext>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void AllThreeContractsResultInSameObject()
		{
			var c1 = _context.GetComponent<IComposer>();
			var c2 = _context.GetComponent<IComponentContext>();
			var c3 = _context.GetComponent<ComponentContext>();

			Assert.AreSame(c1, c2);
			Assert.AreSame(c2, c3);
		}

	}
}
