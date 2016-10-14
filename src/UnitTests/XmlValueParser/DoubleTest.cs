using System;
using Compositional.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class DoubleTest
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
		public void DoubleAttribute()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ADouble.xml");

			var o = _context.GetVariable("doublepi");

			Assert.IsNotNull(o);

			Assert.IsInstanceOfType(o, typeof(double));

			var d = (double)o;

			Assert.AreEqual(d, 3.14159265358979);
		}

		[TestMethod]
		public void DoubleAttributeNaN()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ADouble.xml");

			var o = _context.GetVariable("doubleNaN");

			Assert.IsNotNull(o);

			Assert.IsInstanceOfType(o, typeof(double));

			var d = (double)o;

			Assert.AreEqual(d, double.NaN);
		}

		[TestMethod]
		public void DoubleAttributeZero()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ADouble.xml");

			var oPlus = _context.GetVariable("double+0");
			var oMinus = _context.GetVariable("double-0");

			Assert.IsNotNull(oPlus);
			Assert.IsNotNull(oMinus);

			Assert.IsInstanceOfType(oPlus, typeof(double));
			Assert.IsInstanceOfType(oMinus, typeof(double));

			var dPlus = (double)oPlus;
			var dMinus = (double)oMinus;

			Assert.AreEqual(dPlus, +0);
			Assert.AreEqual(dMinus, -0);
			Assert.AreEqual(dPlus, dMinus);
		}

		[TestMethod]
		public void DoubleAttributeInfinity()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ADouble.xml");

			var oPlus = _context.GetVariable("double+inf");
			var oMinus = _context.GetVariable("double-inf");

			Assert.IsNotNull(oPlus);
			Assert.IsNotNull(oMinus);

			Assert.IsInstanceOfType(oPlus, typeof(double));
			Assert.IsInstanceOfType(oMinus, typeof(double));

			var dPlus = (double)oPlus;
			var dMinus = (double)oMinus;

			Assert.AreEqual(dPlus, Double.PositiveInfinity);
			Assert.AreEqual(dMinus, Double.NegativeInfinity);
		}

		[TestMethod]
		public void DoubleElement()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EDouble.xml");

			var o = _context.GetVariable("doublepi");

			Assert.IsNotNull(o);

			Assert.IsInstanceOfType(o, typeof(double));

			var d = (double)o;

			Assert.AreEqual(d, 3.14159265358979);
		}

		[TestMethod]
		public void DoubleElementNaN()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EDouble.xml");

			var o = _context.GetVariable("doubleNaN");

			Assert.IsNotNull(o);

			Assert.IsInstanceOfType(o, typeof(double));

			var d = (double)o;

			Assert.AreEqual(d, double.NaN);
		}

		[TestMethod]
		public void DoubleElementZero()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EDouble.xml");

			var oPlus = _context.GetVariable("double+0");
			var oMinus = _context.GetVariable("double-0");

			Assert.IsNotNull(oPlus);
			Assert.IsNotNull(oMinus);

			Assert.IsInstanceOfType(oPlus, typeof(double));
			Assert.IsInstanceOfType(oMinus, typeof(double));

			var dPlus = (double)oPlus;
			var dMinus = (double)oMinus;

			Assert.AreEqual(dPlus, +0);
			Assert.AreEqual(dMinus, -0);
			Assert.AreEqual(dPlus, dMinus);
		}

		[TestMethod]
		public void DoubleElementInfinity()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EDouble.xml");

			var oPlus = _context.GetVariable("double+inf");
			var oMinus = _context.GetVariable("double-inf");

			Assert.IsNotNull(oPlus);
			Assert.IsNotNull(oMinus);

			Assert.IsInstanceOfType(oPlus, typeof(double));
			Assert.IsInstanceOfType(oMinus, typeof(double));

			var dPlus = (double)oPlus;
			var dMinus = (double)oMinus;

			Assert.AreEqual(dPlus, Double.PositiveInfinity);
			Assert.AreEqual(dMinus, Double.NegativeInfinity);
		}
	}
}
