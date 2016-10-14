using Compositional.Composer.UnitTests.TestAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compositional.Composer.Utility;

namespace Compositional.Composer.UnitTests.AssemblyTests
{
	[TestClass]
	public class AssemblyUtilTest
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
		public void RegisterWithAssemblyObject()
		{
			_context.RegisterAssembly(typeof (IContractInAnotherAssembly).Assembly);

			var c = _context.GetComponent<IContractInAnotherAssembly>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void RegisterWithAssemblyName()
		{
			_context.RegisterAssembly("Compositional.Composer.UnitTests.TestAssembly");

			var c = _context.GetComponent<IContractInAnotherAssembly>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void RegisterWithAssemblyPath()
		{
			_context.RegisterAssemblyFile(typeof (IContractInAnotherAssembly).Assembly.Location);

			var c = _context.GetComponent<IContractInAnotherAssembly>();

			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void CheckIgnoredComponents()
		{
			_context.RegisterAssembly(typeof(IContractInAnotherAssembly).Assembly);

			var c1 = _context.GetComponent<IContractInAnotherAssembly>();
			Assert.IsNotNull(c1);

			var c2 = _context.GetComponent<IIgnoredContract>();
			Assert.IsNull(c2);
		}
	}
}
