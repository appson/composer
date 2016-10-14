using Appson.Composer.UnitTests.InitializationPointVariety.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.InitializationPointVariety
{
	[TestClass]
	public class ConfigurationPointTest
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
			_context.SetVariableValue("SomeConfigurationPoint", "SomeConfigurationValue");
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void ConfigurationPointAsField()
		{
			_context.Register(typeof(WithFieldConfigurationPoint));

			var c = _context.GetComponent<WithFieldConfigurationPoint>();

			Assert.AreEqual(c.SomeConfigurationPoint, "SomeConfigurationValue");
		}

		[TestMethod]
		public void ConfigurationPointAsProperty()
		{
			_context.Register(typeof(WithPropertyConfigurationPoint));

			var c = _context.GetComponent<WithPropertyConfigurationPoint>();

			Assert.AreEqual(c.SomeConfigurationPoint, "SomeConfigurationValue");
		}
	}
}
