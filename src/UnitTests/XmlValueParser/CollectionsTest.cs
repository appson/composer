using System.Collections.Generic;
using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.XmlValueParser
{
	[TestClass]
	public class CollectionsTest
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
		public void AttributeEmptyIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("emptyIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[]) o;

			Assert.AreEqual(iArray.Length, 0);
		}

		[TestMethod]
		public void AttributeEmptyStringArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("emptyStringArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(string[]));

			var sArray = (string[]) o;

			Assert.AreEqual(sArray.Length, 0);
		}

		[TestMethod]
		public void AttributeSingleIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("singleIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[]) o;

			Assert.AreEqual(iArray.Length, 1);
			Assert.AreEqual(iArray[0], 0);
		}

		[TestMethod]
		public void AttributeSingleStringArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("singleStringArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(string[]));

			var sArray = (string[]) o;

			Assert.AreEqual(sArray.Length, 1);
			Assert.AreEqual(sArray[0], "zero");
		}

		[TestMethod]
		public void AttributeFourElementIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("fourElementIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[])o;

			Assert.AreEqual(iArray.Length, 4);
			for (int i = 0; i < 4; i++)
				Assert.AreEqual(iArray[i], i);
		}

		[TestMethod]
		public void AttributeEmptyListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("emptyListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>) o;

			Assert.AreEqual(iList.Count, 0);
		}

		[TestMethod]
		public void AttributeEmptyListOfString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("emptyListOfString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<string>));

			var sList = (List<string>) o;

			Assert.AreEqual(sList.Count, 0);
		}

		[TestMethod]
		public void AttributeSingleListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("singleListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>) o;

			Assert.AreEqual(iList.Count, 1);
			Assert.AreEqual(iList[0], 0);
		}

		[TestMethod]
		public void AttributeSingleListOfString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("singleListOfString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<string>));

			var sList = (List<string>) o;

			Assert.AreEqual(sList.Count, 1);
			Assert.AreEqual(sList[0], "zero");
		}

		[TestMethod]
		public void AttributeFourElementListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("fourElementListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>)o;

			Assert.AreEqual(iList.Count, 4);
			for (int i = 0; i < 4; i++)
				Assert.AreEqual(iList[i], i);
		}

		[TestMethod]
		public void AttributeEmptyDictionary()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("emptyDictionary");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(Dictionary<int, string>));

			var dic = (Dictionary<int, string>)o;

			Assert.AreEqual(dic.Count, 0);
		}

		[TestMethod]
		public void AttributeFilledDictionary()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ACollection.xml");

			var o = _context.GetVariable("filledDictionary");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(Dictionary<int, string>));

			var dic = (Dictionary<int, string>)o;

			Assert.AreEqual(dic.Count, 4);
			Assert.AreEqual(dic[1], "one");
			Assert.AreEqual(dic[3], "three");
			Assert.AreEqual(dic[5], "five");
			Assert.AreEqual(dic[7], "seven");
		}

		[TestMethod]
		public void ElementEmptyIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("emptyIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[]) o;

			Assert.AreEqual(iArray.Length, 0);
		}

		[TestMethod]
		public void ElementEmptyStringArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("emptyStringArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(string[]));

			var sArray = (string[]) o;

			Assert.AreEqual(sArray.Length, 0);
		}

		[TestMethod]
		public void ElementSingleIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("singleIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[]) o;

			Assert.AreEqual(iArray.Length, 1);
			Assert.AreEqual(iArray[0], 0);
		}

		[TestMethod]
		public void ElementSingleStringArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("singleStringArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(string[]));

			var sArray = (string[]) o;

			Assert.AreEqual(sArray.Length, 1);
			Assert.AreEqual(sArray[0].Trim(), "zero");
		}

		[TestMethod]
		public void ElementFourElementIntArray()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("fourElementIntArray");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(int[]));

			var iArray = (int[])o;

			Assert.AreEqual(iArray.Length, 4);
			for (int i = 0; i < 4; i++)
				Assert.AreEqual(iArray[i], i);
		}

		[TestMethod]
		public void ElementEmptyListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("emptyListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>) o;

			Assert.AreEqual(iList.Count, 0);
		}

		[TestMethod]
		public void ElementEmptyListOfString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("emptyListOfString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<string>));

			var sList = (List<string>) o;

			Assert.AreEqual(sList.Count, 0);
		}

		[TestMethod]
		public void ElementSingleListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("singleListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>) o;

			Assert.AreEqual(iList.Count, 1);
			Assert.AreEqual(iList[0], 0);
		}

		[TestMethod]
		public void ElementSingleListOfString()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("singleListOfString");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<string>));

			var sList = (List<string>) o;

			Assert.AreEqual(sList.Count, 1);
			Assert.AreEqual(sList[0].Trim(), "zero");
		}

		[TestMethod]
		public void ElementFourElementListOfInt()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("fourElementListOfInt");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(List<int>));

			var iList = (List<int>)o;

			Assert.AreEqual(iList.Count, 4);
			for (int i = 0; i < 4; i++)
				Assert.AreEqual(iList[i], i);
		}

		[TestMethod]
		public void ElementEmptyDictionary()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("emptyDictionary");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(Dictionary<int, string>));

			var dic = (Dictionary<int, string>)o;

			Assert.AreEqual(dic.Count, 0);
		}

		[TestMethod]
		public void ElementFilledDictionary()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.XmlValueParser.Xmls.ECollection.xml");

			var o = _context.GetVariable("filledDictionary");

			Assert.IsNotNull(o);
			Assert.IsInstanceOfType(o, typeof(Dictionary<int, string>));

			var dic = (Dictionary<int, string>)o;

			Assert.AreEqual(dic.Count, 4);
			Assert.AreEqual(dic[1].Trim(), "one");
			Assert.AreEqual(dic[3].Trim(), "three");
			Assert.AreEqual(dic[5].Trim(), "five");
			Assert.AreEqual(dic[7].Trim(), "seven");
		}


	}
}
