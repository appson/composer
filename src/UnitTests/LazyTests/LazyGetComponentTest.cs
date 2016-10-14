using Appson.Composer.UnitTests.LazyTests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appson.Composer.Utility;

namespace Appson.Composer.UnitTests.LazyTests
{
	[TestClass]
	public class LazyGetComponentTest
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
		public void GenericGetComponentWithoutName()
		{
			var cLazy = _context.LazyGetComponent<ISampleContract>();
			_context.Register(typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void GenericGetComponentWithName()
		{
			var cLazy = _context.LazyGetComponent<ISampleContract>("componentName");
			_context.Register("componentName", typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
		}
		
		[TestMethod]
		public void GetComponentWithoutName()
		{
			var cLazy = _context.LazyGetComponent(typeof(ISampleContract));
			_context.Register(typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
		}
		
		[TestMethod]
		public void GetComponentWithName()
		{
			var cLazy = _context.LazyGetComponent(typeof(ISampleContract), "componentName");
			_context.Register("componentName", typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
		}
	}
}
