using Compositional.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class UnsignedIntTest
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
		public void AttributeByteMax()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("bytemax");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(byte));

			var i = (byte)o;
			Assert.AreEqual(i, 255);
		}

		[TestMethod]
		public void AttributeByteOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("byte1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(byte));

			var i = (byte)o;
			Assert.AreEqual(i, 1);
		}

		[TestMethod]
		public void AttributeUInt16Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint16max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ushort));

			var i = (ushort)o;
			Assert.AreEqual(i, 65535);
		}

		[TestMethod]
		public void AttributeUInt16One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint161");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ushort));

			var i = (ushort)o;
			Assert.AreEqual(i, 1);
		}

		[TestMethod]
		public void AttributeUInt32Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint32max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(uint));

			var i = (uint)o;
			Assert.AreEqual(i, 4294967295);
		}

		[TestMethod]
		public void AttributeUInt32One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint321");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(uint));

			var i = (uint)o;
			Assert.AreEqual(i, (uint)1);
		}

		[TestMethod]
		public void AttributeUInt64Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint64max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ulong));

			var i = (ulong)o;
			Assert.AreEqual(i, 18446744073709551615);
		}

		[TestMethod]
		public void AttributeUInt64One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AUInt.xml");

			var o = _context.GetVariable("uint641");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ulong));

			var i = (ulong)o;
			Assert.AreEqual(i, (ulong)1);
		}

		[TestMethod]
		public void ElementByteMax()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("bytemax");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(byte));

			var i = (byte)o;
			Assert.AreEqual(i, 255);
		}

		[TestMethod]
		public void ElementByteOne()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("byte1");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(byte));

			var i = (byte)o;
			Assert.AreEqual(i, 1);
		}

		[TestMethod]
		public void ElementUInt16Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint16max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ushort));

			var i = (ushort)o;
			Assert.AreEqual(i, 65535);
		}

		[TestMethod]
		public void ElementUInt16One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint161");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ushort));

			var i = (ushort)o;
			Assert.AreEqual(i, 1);
		}

		[TestMethod]
		public void ElementUInt32Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint32max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(uint));

			var i = (uint)o;
			Assert.AreEqual(i, 4294967295);
		}

		[TestMethod]
		public void ElementUInt32One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint321");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(uint));

			var i = (uint)o;
			Assert.AreEqual(i, (uint)1);
		}

		[TestMethod]
		public void ElementUInt64Max()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint64max");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ulong));

			var i = (ulong)o;
			Assert.AreEqual(i, 18446744073709551615);
		}

		[TestMethod]
		public void ElementUInt64One()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EUInt.xml");

			var o = _context.GetVariable("uint641");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(ulong));

			var i = (ulong)o;
			Assert.AreEqual(i, (ulong)1);
		}

	}
}
