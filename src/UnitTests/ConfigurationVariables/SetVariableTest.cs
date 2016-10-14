using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.ConfigurationVariables
{
	[TestClass]
	public class SetVariableTest
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
		public void SetDirectValue()
		{
			_context.SetVariableValue("variable", "variableValue");

			var v = _context.GetVariable("variable");

			Assert.IsNotNull(v);
			Assert.IsInstanceOfType(v, typeof(string));
			Assert.AreEqual(v, "variableValue");
		}

		[TestMethod]
		public void SetLazyValue()
		{
			_context.SetVariable("variable", new Lazy<object>(() => "variableValue"));

			var v = _context.GetVariable("variable");

			Assert.IsNotNull(v);
			Assert.IsInstanceOfType(v, typeof(string));
			Assert.AreEqual(v, "variableValue");
		}

		[TestMethod]
		public void SetLazyValueAndChange()
		{
			var s = "variableValue";

// ReSharper disable AccessToModifiedClosure
			_context.SetVariable("variable", new Lazy<object>(() => s));
// ReSharper restore AccessToModifiedClosure

			s = "variableNewValue";

			var v = _context.GetVariable("variable");

			Assert.IsNotNull(v);
			Assert.IsInstanceOfType(v, typeof(string));
			Assert.AreNotEqual(v, "variableValue");
			Assert.AreEqual(v, "variableNewValue");
		}

	}
}
