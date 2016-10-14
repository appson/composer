using System.Linq;
using Appson.Composer.UnitTests.ComponentInstantiations.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ComponentInstantiations
{
	[TestClass]
	public class SharedPerContractTest
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
			_context.Register(typeof(SpcComponent));
			_context.Register(typeof(SpcComponentWithPlugs));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void RegisterTwoTimesQueryBySelf()
		{
			_context.Register(typeof(SpcComponent)); // Register for second time

			var all = _context.GetAllComponents<SpcComponent>();
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
			_context.Register(typeof(SpcComponent));

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
			var c2 = _context.GetComponent<SpcComponent>();

			Assert.AreNotSame(c0, c1);
			Assert.AreNotSame(c0, c2);
			Assert.AreNotSame(c1, c2);
		}

		[TestMethod]
		public void QueryTwoTimesByContract()
		{
			var c0 = _context.GetComponent<ISomeContract>();
			var c1 = _context.GetComponent<ISomeContract>();

			Assert.AreSame(c0, c1);
		}

		[TestMethod]
		public void QueryTwoTimesBySelf()
		{
			var c0 = _context.GetComponent<SpcComponent>();
			var c1 = _context.GetComponent<SpcComponent>();

			Assert.AreSame(c0, c1);
		}

		[TestMethod]
		public void QueryTwoTimesIndirect()
		{
			_context.Register(typeof(SpcComponentWithPlugs));
			_context.Register(typeof(SprComponent));
			_context.Register(typeof(NonSharedComponent));

			var all = _context.GetAllComponents<SpcComponentWithPlugs>();
			Assert.IsNotNull(all);
			Assert.IsTrue(all.Count() == 2);

			var allArray = all.ToArray();
			var c0 = allArray[0];
			var c1 = allArray[1];
			Assert.AreNotSame(c0, c1);
			Assert.AreSame(c0.SpcComponent, c1.SpcComponent);
		}
	}
}
