using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class BoolTest
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
		public void BooleanAttributeNumeric()
		{
			_context.ProcessCompositionXmlFromResource("Appson.Composer.UnitTests.XmlValueParser.Xmls.ABool.xml");

			var o0 = _context.GetVariable("bool0");
			var o1 = _context.GetVariable("bool1");

			Assert.IsNotNull(o0);
			Assert.IsNotNull(o1);

			Assert.IsInstanceOfType(o0, typeof(bool));
			Assert.IsInstanceOfType(o1, typeof(bool));

			var b0 = (bool) o0;
			var b1 = (bool) o1;

			Assert.AreEqual(b0, false);
			Assert.AreEqual(b1, true);
		}

		[TestMethod]
		public void BooleanAttributeString()
		{
			_context.ProcessCompositionXmlFromResource("Appson.Composer.UnitTests.XmlValueParser.Xmls.ABool.xml");

			var oTrue = _context.GetVariable("boolTrue");
			var oFalse = _context.GetVariable("boolFalse");

			Assert.IsNotNull(oTrue);
			Assert.IsNotNull(oFalse);

			Assert.IsInstanceOfType(oTrue, typeof(bool));
			Assert.IsInstanceOfType(oFalse, typeof(bool));

			var bTrue = (bool) oTrue;
			var bFalse = (bool) oFalse;

			Assert.AreEqual(bTrue, true);
			Assert.AreEqual(bFalse, false);
		}

		[TestMethod]
		public void BooleanElementNumeric()
		{
			_context.ProcessCompositionXmlFromResource("Appson.Composer.UnitTests.XmlValueParser.Xmls.EBool.xml");

			var o0 = _context.GetVariable("bool0");
			var o1 = _context.GetVariable("bool1");

			Assert.IsNotNull(o0);
			Assert.IsNotNull(o1);

			Assert.IsInstanceOfType(o0, typeof(bool));
			Assert.IsInstanceOfType(o1, typeof(bool));

			var b0 = (bool) o0;
			var b1 = (bool) o1;

			Assert.AreEqual(b0, false);
			Assert.AreEqual(b1, true);
		}

		[TestMethod]
		public void BooleanElementString()
		{
			_context.ProcessCompositionXmlFromResource("Appson.Composer.UnitTests.XmlValueParser.Xmls.EBool.xml");

			var oTrue = _context.GetVariable("boolTrue");
			var oFalse = _context.GetVariable("boolFalse");

			Assert.IsNotNull(oTrue);
			Assert.IsNotNull(oFalse);

			Assert.IsInstanceOfType(oTrue, typeof(bool));
			Assert.IsInstanceOfType(oFalse, typeof(bool));

			var bTrue = (bool) oTrue;
			var bFalse = (bool) oFalse;

			Assert.AreEqual(bTrue, true);
			Assert.AreEqual(bFalse, false);
		}

	}
}
