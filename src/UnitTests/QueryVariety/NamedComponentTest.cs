using System.Linq;
using Compositional.Composer.UnitTests.QueryVariety.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.QueryVariety
{
	[TestClass]
	public class NamedComponentTest
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
			_context.Register("someName", typeof(SampleComponent));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void GetComponentOfT()
		{
			var c = _context.GetComponent<ISampleContract>();
			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetComponentOfTWithNullName()
		{
			var c = _context.GetComponent<ISampleContract>(null);
			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetComponentOfTWithSomeName()
		{
			var c = _context.GetComponent<ISampleContract>("someName");
			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void GetComponentWithType()
		{
			var c = _context.GetComponent(typeof(ISampleContract));
			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetComponentWithTypeAndNullName()
		{
			var c = _context.GetComponent(typeof(ISampleContract), null);
			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetComponentWithTypeAndSomeName()
		{
			var c = _context.GetComponent(typeof(ISampleContract), "someName");
			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void GetAllComponentsOfT()
		{
			var cs = _context.GetAllComponents<ISampleContract>();
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 0);
		}

		[TestMethod]
		public void GetAllComponentsOfTWithNullName()
		{
			var cs = _context.GetAllComponents<ISampleContract>(null);
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 0);
		}

		[TestMethod]
		public void GetAllComponentsOfTWithSomeName()
		{
			var cs = _context.GetAllComponents<ISampleContract>("someName");
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 1);
			Assert.IsNotNull(cs.First());
		}

		[TestMethod]
		public void GetAllComponentsWithType()
		{
			var cs = _context.GetAllComponents(typeof(ISampleContract));
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 0);
		}

		[TestMethod]
		public void GetAllComponentsWithTypeAndNullName()
		{
			var cs = _context.GetAllComponents(typeof(ISampleContract), null);
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 0);
		}

		[TestMethod]
		public void GetAllComponentWithTypeAndSomeName()
		{
			var cs = _context.GetAllComponents(typeof(ISampleContract), "someName");
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 1);
			Assert.IsNotNull(cs.First());
		}

		[TestMethod]
		public void GetComponentFamilyOfT()
		{
			var cs = _context.GetComponentFamily<ISampleContract>();
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 1);
			Assert.IsNotNull(cs.First());
		}

		[TestMethod]
		public void GetComponentFamilyWithType()
		{
			var cs = _context.GetComponentFamily(typeof(ISampleContract));
			Assert.IsNotNull(cs);
			Assert.IsTrue(cs.Count() == 1);
			Assert.IsNotNull(cs.First());
		}
	}
}
