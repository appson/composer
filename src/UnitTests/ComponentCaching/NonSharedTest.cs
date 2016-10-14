using System.Linq;
using Compositional.Composer.UnitTests.ComponentInstantiations.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.ComponentInstantiations
{
	[TestClass]
	public class NonSharedTest
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
			_context.Register(typeof(NonSharedComponent));
			_context.Register(typeof(NonSharedComponentWithPlugs));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void RegisterTwoTimesQueryBySelf()
		{
			_context.Register(typeof(NonSharedComponent)); // Register for second time

			var all = _context.GetAllComponents<NonSharedComponent>();
			Assert.IsNotNull(all);
			Assert.IsTrue(all.Count() == 2);

			var allArray = all.ToArray();
			var c0 = allArray[0];
			var c1 = allArray[1];
			Assert.AreNotSame(c0, c1);
		}

		[TestMethod]
		public void RegisterTwoTimesQueryByContract()
		{
			_context.Register(typeof(NonSharedComponent));

			var all = _context.GetAllComponents<ISomeContract>();
			Assert.IsNotNull(all);
			Assert.IsTrue(all.Count() == 2);

			var allArray = all.ToArray();
			var c0 = allArray[0];
			var c1 = allArray[1];
			Assert.AreNotSame(c0, c1);
		}

		[TestMethod]
		public void CheckDifferentContracts()
		{
			var c0 = _context.GetComponent<ISomeContract>();
			var c1 = _context.GetComponent<IAnotherContract>();
			var c2 = _context.GetComponent<NonSharedComponent>();

			Assert.AreNotSame(c0, c1);
			Assert.AreNotSame(c0, c2);
			Assert.AreNotSame(c1, c2);
		}

		[TestMethod]
		public void QueryTwoTimesByContract()
		{
			var c0 = _context.GetComponent<ISomeContract>();
			var c1 = _context.GetComponent<ISomeContract>();

			Assert.AreNotSame(c0, c1);
		}

		[TestMethod]
		public void QueryTwoTimesBySelf()
		{
			var c0 = _context.GetComponent<NonSharedComponent>();
			var c1 = _context.GetComponent<NonSharedComponent>();

			Assert.AreNotSame(c0, c1);
		}

		[TestMethod]
		public void QueryTwoTimesIndirect()
		{
			_context.Register(typeof(NonSharedComponentWithPlugs));
			_context.Register(typeof(SprComponent));
			_context.Register(typeof(SpcComponent));

			var all = _context.GetAllComponents<NonSharedComponentWithPlugs>();
			Assert.IsNotNull(all);
			Assert.IsTrue(all.Count() == 2);

			var allArray = all.ToArray();
			var c0 = allArray[0];
			var c1 = allArray[1];
			Assert.AreNotSame(c0, c1);
			Assert.AreNotSame(c0.NonSharedComponent, c1.NonSharedComponent);
		}
	}
}
