using Appson.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Appson.Composer.UnitTests.LazyTests.Components;

namespace Appson.Composer.UnitTests.LazyTests
{
	[TestClass]
	public class LazyGetAllComponentsTest
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
		public void GenericGetAllComponentsWithoutName()
		{
			var cLazy = _context.LazyGetAllComponents<ISampleContract>();
			_context.Register(typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
			Assert.AreEqual(1, c.Count());
		}

		[TestMethod]
		public void GenericGetAllComponentsWithName()
		{
			var cLazy = _context.LazyGetAllComponents<ISampleContract>("componentName");
			_context.Register("componentName", typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
			Assert.AreEqual(1, c.Count());
		}

		[TestMethod]
		public void GetAllComponentsWithoutName()
		{
			var cLazy = _context.LazyGetAllComponents(typeof(ISampleContract));
			_context.Register(typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
			Assert.AreEqual(1, c.Count());
		}

		[TestMethod]
		public void GetAllComponentsWithName()
		{
			var cLazy = _context.LazyGetAllComponents(typeof(ISampleContract), "componentName");
			_context.Register("componentName", typeof(SampleComponent));

			var c = cLazy.Value;

			Assert.IsNotNull(c);
			Assert.AreEqual(1, c.Count());
		}
	}
}
