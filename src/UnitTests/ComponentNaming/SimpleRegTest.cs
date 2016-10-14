using Appson.Composer.UnitTests.ComponentNaming.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ComponentNaming
{
	[TestClass]
	public class SimpleRegTest
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
			_context.Register(typeof(ComponentWithDefaultName));
			_context.Register(typeof(ComponentWithoutName));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void QueryWithoutName()
		{
			var c1 = _context.GetComponent<ComponentWithDefaultName>();
			var c2 = _context.GetComponent<ComponentWithoutName>();
			Assert.IsNull(c1);
			Assert.IsNotNull(c2);
		}

		[TestMethod]
		public void QueryWithDefaultName()
		{
			var c1 = _context.GetComponent<ComponentWithDefaultName>("defaultName");
			var c2 = _context.GetComponent<ComponentWithoutName>("defaultName");
			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
		}

		[TestMethod]
		public void QueryWithNullName()
		{
			var c1 = _context.GetComponent<ComponentWithDefaultName>(null);
			var c2 = _context.GetComponent<ComponentWithoutName>(null);
			Assert.IsNull(c1);
			Assert.IsNotNull(c2);
		}

		[TestMethod]
		public void QueryWithDifferentName()
		{
			var c1 = _context.GetComponent<ComponentWithDefaultName>("differentName");
			var c2 = _context.GetComponent<ComponentWithoutName>("differentName");
			Assert.IsNull(c1);
			Assert.IsNull(c2);
		}
	}
}
