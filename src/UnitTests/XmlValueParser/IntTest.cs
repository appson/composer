using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class IntTest
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
		public void AttributeSignedByteMax()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("sbytemax");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(sbyte));

			var i = (sbyte)o;
			Assert.AreEqual(i, 127);
		}

		[TestMethod]
		public void AttributeSignedByteMinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("sbyte-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(sbyte));

			var i = (sbyte)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void AttributeInt16Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int16max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(short));

			var i = (short)o;
			Assert.AreEqual(i, 32767);
		}

		[TestMethod]
		public void AttributeInt16MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int16-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(short));

			var i = (short)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void AttributeInt32Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int32max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int));

			var i = (int)o;
			Assert.AreEqual(i, 2147483647);
		}

		[TestMethod]
		public void AttributeInt32MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int32-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int));

			var i = (int)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void AttributeInt64Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int64max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(long));

			var i = (long)o;
			Assert.AreEqual(i, 9223372036854775807);
		}

		[TestMethod]
		public void AttributeInt64MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AInt.xml");

			var o = _context.GetVariable("int64-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(long));

			var i = (long)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void ElementSignedByteMax()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("sbytemax");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(sbyte));

			var i = (sbyte)o;
			Assert.AreEqual(i, 127);
		}

		[TestMethod]
		public void ElementSignedByteMinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("sbyte-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(sbyte));

			var i = (sbyte)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void ElementInt16Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int16max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(short));

			var i = (short)o;
			Assert.AreEqual(i, 32767);
		}

		[TestMethod]
		public void ElementInt16MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int16-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(short));

			var i = (short)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void ElementInt32Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int32max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int));

			var i = (int)o;
			Assert.AreEqual(i, 2147483647);
		}

		[TestMethod]
		public void ElementInt32MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int32-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int));

			var i = (int)o;
			Assert.AreEqual(i, -1);
		}

		[TestMethod]
		public void ElementInt64Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int64max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(long));

			var i = (long)o;
			Assert.AreEqual(i, 9223372036854775807);
		}

		[TestMethod]
		public void ElementInt64MinusOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EInt.xml");

			var o = _context.GetVariable("int64-1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(long));

			var i = (long)o;
			Assert.AreEqual(i, -1);
		}
	}
}
