using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appson.Composer.UnitTests.XmlValueParser.Components;
using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class ObjectTest
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
			_context.Register(typeof(SampleComponent));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void AttributeSimpleObject()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("simpleObject");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);
		}

		[TestMethod]
		public void AttributeObjectWithPlugs()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("objectWithPlugs");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNotNull(sc.SampleContract);
		}

		[TestMethod]
		public void AttributeObjectWithField()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("objectWithField");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNotNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.Field, "FieldValue");
		}

		[TestMethod]
		public void AttributeObjectWithProperty()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("objectWithProperty");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNotNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.Property, "PropertyValue");
		}

		[TestMethod]
		public void AttributeObjectWithConstructorArg()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("objectWithConstructorArg");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNotNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.ConstructorArg, "ConstructorArgValue");
		}

		[TestMethod]
		public void AttributeObjectWithEverything()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.AObject.xml");

			var o = _context.GetVariable("objectWithEverything");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNotNull(sc.Field);
			Assert.IsNotNull(sc.Property);
			Assert.IsNotNull(sc.ConstructorArg);
			Assert.IsNotNull(sc.SampleContract);

			Assert.AreEqual(sc.Field, "FieldValue");
			Assert.AreEqual(sc.Property, "PropertyValue");
			Assert.AreEqual(sc.ConstructorArg, "ConstructorArgValue");
		}

		[TestMethod]
		public void ElementSimpleObject()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("simpleObject");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);
		}

		[TestMethod]
		public void ElementObjectWithPlugs()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("objectWithPlugs");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNotNull(sc.SampleContract);
		}

		[TestMethod]
		public void ElementObjectWithField()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("objectWithField");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNotNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.Field, "FieldValue");
		}

		[TestMethod]
		public void ElementObjectWithProperty()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("objectWithProperty");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNotNull(sc.Property);
			Assert.IsNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.Property, "PropertyValue");
		}

		[TestMethod]
		public void ElementObjectWithConstructorArg()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("objectWithConstructorArg");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNull(sc.Field);
			Assert.IsNull(sc.Property);
			Assert.IsNotNull(sc.ConstructorArg);
			Assert.IsNull(sc.SampleContract);

			Assert.AreEqual(sc.ConstructorArg, "ConstructorArgValue");
		}

		[TestMethod]
		public void ElementObjectWithEverything()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.EObject.xml");

			var o = _context.GetVariable("objectWithEverything");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(SampleClass));

			var sc = (SampleClass) o;
			Assert.IsNotNull(sc.Field);
			Assert.IsNotNull(sc.Property);
			Assert.IsNotNull(sc.ConstructorArg);
			Assert.IsNotNull(sc.SampleContract);

			Assert.AreEqual(sc.Field, "FieldValue");
			Assert.AreEqual(sc.Property, "PropertyValue");
			Assert.AreEqual(sc.ConstructorArg, "ConstructorArgValue");
		}
	}
}
