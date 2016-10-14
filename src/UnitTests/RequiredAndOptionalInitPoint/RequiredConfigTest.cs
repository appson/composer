using Appson.Composer.UnitTests.RequiredAndOptionalInitPoint.Components;
using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.RequiredAndOptionalInitPoint
{
	[TestClass]
	public class RequiredConfigTest
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
		public void ReqConfigProvided()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Xmls.ReqConfigProvided.xml");

			var c = _context.GetComponent<ComponentWithRequiredConfig>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.SomeConfig);
			Assert.AreEqual(c.SomeConfig, "SomeConfigValue");
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqConfigNotProvided()
		{
			_context.Register(typeof(ComponentWithRequiredConfig));
		}

		[TestMethod]
		public void ReqNamedConfigProvidedByVar()
		{
			_context.Register(typeof(ComponentWithRequiredNamedConfig));
			_context.SetVariableValue("someVariable", "SomeConfigValue");

			var c = _context.GetComponent<ComponentWithRequiredNamedConfig>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.SomeConfig);
			Assert.AreEqual(c.SomeConfig, "SomeConfigValue");
		}

		[TestMethod]
		public void ReqNamedConfigProvidedByReg()
		{
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Xmls.ReqNamedConfigProvidedByReg.xml");

			var c = _context.GetComponent<ComponentWithRequiredNamedConfig>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.SomeConfig);
			Assert.AreEqual(c.SomeConfig, "SomeConfigValue");
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqNamedConfigNotProvided()
		{
			_context.Register(typeof(ComponentWithRequiredNamedConfig));

			_context.GetComponent<ComponentWithRequiredNamedConfig>();
		}
	}
}
