using Appson.Composer.Factories;
using Appson.Composer.UnitTests.RegisterVariety.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.RegisterVariety
{
	[TestClass]
	public class RegisterVarietyTest
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
		public void RegisterSingleProvided()
		{
			_context.Register(typeof(ComponentOne));

			var c = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void RegisterMultiProvided()
		{
			_context.Register(typeof(ComponentBoth));

			var c1 = _context.GetComponent<IContractOne>();
			var c2 = _context.GetComponent<IContractTwo>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
		}

		[TestMethod]
		public void RegisterWithNamedContract()
		{
			_context.Register("someName", typeof(ComponentBoth));

			var c1 = _context.GetComponent<IContractOne>("someName");
			var c2 = _context.GetComponent<IContractTwo>("someName");
			var cNoName = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
			Assert.IsNull(cNoName);
		}

		[TestMethod]
		public void RegisterSingleProvidedWithSpecificContract()
		{
			_context.Register(typeof(IContractOne), typeof(ComponentOne));

			var c = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void RegisterMultiProvidedWithSpecificContract()
		{
			_context.Register(typeof(IContractOne), typeof(ComponentBoth));

			var c1 = _context.GetComponent<IContractOne>();
			var c2 = _context.GetComponent<IContractTwo>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
		}

		[TestMethod]
		public void RegisterWithSpecificNamedContract()
		{
			_context.Register(typeof(IContractOne), "someName", typeof(ComponentBoth));

			var c1 = _context.GetComponent<IContractOne>("someName");
			var c2 = _context.GetComponent<IContractTwo>("someName");
			var cNoName = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(cNoName);
		}

		[TestMethod]
		public void RegisterFactory()
		{
			var f = new LocalComponentFactory(typeof(ComponentBoth));
			_context.Register(f);

			var c1 = _context.GetComponent<IContractOne>();
			var c2 = _context.GetComponent<IContractTwo>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
		}

		[TestMethod]
		public void RegisterFactoryWithName()
		{
			var f = new LocalComponentFactory(typeof(ComponentBoth));
			_context.Register("someName", f);

			var c1 = _context.GetComponent<IContractOne>("someName");
			var c2 = _context.GetComponent<IContractTwo>("someName");
			var cNoName = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
			Assert.IsNull(cNoName);
		}

		[TestMethod]
		public void RegisterFactoryWithSpecificContract()
		{
			var f = new LocalComponentFactory(typeof (ComponentBoth));
			_context.Register(typeof(IContractOne), f);

			var c1 = _context.GetComponent<IContractOne>();
			var c2 = _context.GetComponent<IContractTwo>();
			
			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
		}

		[TestMethod]
		public void RegisterFactoryWithNameAndSpecificContract()
		{
			var f = new LocalComponentFactory(typeof(ComponentBoth));
			_context.Register(typeof(IContractOne), "someName", f);

			var c1 = _context.GetComponent<IContractOne>("someName");
			var c2 = _context.GetComponent<IContractTwo>("someName");
			var cNoName = _context.GetComponent<IContractOne>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(cNoName);
		}
	}
}
