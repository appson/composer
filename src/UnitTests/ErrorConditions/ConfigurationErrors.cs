using Compositional.Composer.UnitTests.ErrorConditions.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.ErrorConditions
{
	[TestClass]
	public class ConfigurationErrors
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
		[ExpectedException(typeof(CompositionException))]
		public void ConfigWithoutSetter()
		{
			_context.Register(typeof(ConfigPointWithoutSetter));
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ConfigWithPrivateSetter()
		{
			_context.Register(typeof(ConfigPointWithPrivateSetter));
		}
	}
}
