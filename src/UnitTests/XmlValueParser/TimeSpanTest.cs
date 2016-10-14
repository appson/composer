using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compositional.Composer.CompositionXml;
using Compositional.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class TimeSpanTest
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
		public void AttributeTimeSpanString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, TimeSpan.Parse("1:00:00"));
		}

		[TestMethod]
		public void AttributeTimeSpanDays()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanDays");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1, 0, 0, 0, 0));
		}

		[TestMethod]
		public void AttributeTimeSpanHours()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanHours");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 1, 0, 0, 0));
		}

		[TestMethod]
		public void AttributeTimeSpanMinutes()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanMinutes");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 1, 0, 0));
		}

		[TestMethod]
		public void AttributeTimeSpanSeconds()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanSeconds");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 0, 1, 0));
		}

		[TestMethod]
		public void AttributeTimeSpanMilliseconds()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanMilliseconds");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 0, 0, 1));
		}

		[TestMethod]
		public void AttributeTimeSpanTicks()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanTicks");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1));
		}

		[TestMethod]
		public void AttributeTimeSpanAll()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			var o = _context.GetVariable("timeSpanAll");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1, 1, 1, 1, 1));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionXmlValidationException))]
		public void AttributeTimeSpanErrorString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			_context.GetVariable("timeSpanErrorString");
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionXmlValidationException))]
		public void AttributeTimeSpanErrorTicks()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ATimeSpan.xml");

			_context.GetVariable("timeSpanErrorTicks");
		}

		[TestMethod]
		public void ElementTimeSpanString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, TimeSpan.Parse("1:00:00"));
		}

		[TestMethod]
		public void ElementTimeSpanDays()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanDays");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1, 0, 0, 0, 0));
		}

		[TestMethod]
		public void ElementTimeSpanHours()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanHours");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 1, 0, 0, 0));
		}

		[TestMethod]
		public void ElementTimeSpanMinutes()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanMinutes");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 1, 0, 0));
		}

		[TestMethod]
		public void ElementTimeSpanSeconds()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanSeconds");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 0, 1, 0));
		}

		[TestMethod]
		public void ElementTimeSpanMilliseconds()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanMilliseconds");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(0, 0, 0, 0, 1));
		}

		[TestMethod]
		public void ElementTimeSpanTicks()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanTicks");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1));
		}

		[TestMethod]
		public void ElementTimeSpanAll()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			var o = _context.GetVariable("timeSpanAll");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(TimeSpan));
			Assert.AreEqual(o, new TimeSpan(1, 1, 1, 1, 1));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionXmlValidationException))]
		public void ElementTimeSpanErrorString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			_context.GetVariable("timeSpanErrorString");
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionXmlValidationException))]
		public void ElementTimeSpanErrorTicks()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ETimeSpan.xml");

			_context.GetVariable("timeSpanErrorTicks");
		}
	}
}
