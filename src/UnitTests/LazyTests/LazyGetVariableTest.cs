using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appson.Composer.Utility;

namespace Appson.Composer.UnitTests.LazyTests
{
	[TestClass]
	public class LazyGetVariableTest
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
		public void GetVariableValue()
		{
			var vLazy = _context.LazyGetVariable("varName");
			_context.SetVariableValue("varName", "varValue");

			var v = vLazy.Value;

			Assert.IsNotNull(v);
			Assert.AreEqual(v, "varValue");
		}
	}
}
