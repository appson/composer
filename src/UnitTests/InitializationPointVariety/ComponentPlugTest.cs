using Appson.Composer.UnitTests.InitializationPointVariety.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.InitializationPointVariety
{
	[TestClass]
	public class ComponentPlugTest
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
		public void ComponentPlugAsField()
		{
			_context.Register(typeof(WithFieldComponentPlug));

			var c = _context.GetComponent<WithFieldComponentPlug>();

			Assert.IsNotNull(c.SampleContract);
		}

		[TestMethod]
		public void ComponentPlugAsProperty()
		{
			_context.Register(typeof(WithPropertyComponentPlug));

			var c = _context.GetComponent<WithPropertyComponentPlug>();

			Assert.IsNotNull(c.SampleContract);
		}
	}
}
