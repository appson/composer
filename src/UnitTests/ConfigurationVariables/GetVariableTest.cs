using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ConfigurationVariables
{
	[TestClass]
	public class GetVariableTest
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
			_context.SetVariableValue("variable", "variableValue");
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void GetAPreviouslySet()
		{
			var v = _context.GetVariable("variable");

			Assert.IsNotNull(v);
			Assert.IsInstanceOfType(v, typeof(string));
			Assert.AreEqual(v, "variableValue");
		}

		[TestMethod]
		public void GetAPreviouslyNotSet()
		{
			var v = _context.GetVariable("someVariableName");

			Assert.IsNull(v);
		}

		[TestMethod]
		public void GetAPreviouslySetAndUnset()
		{
			_context.SetVariableValue("variable", null);

			var v = _context.GetVariable("variable");

			Assert.IsNull(v);
		}
	}
}
