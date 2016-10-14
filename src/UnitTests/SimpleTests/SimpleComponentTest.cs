using Appson.Composer.UnitTests.SimpleTests.SimpleTestComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.SimpleTests
{
	[TestClass]
	public class SimpleComponentTest
	{
		private ComponentContext _context;

		#region Initialization and Cleanup methods

		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
		}

		[TestInitialize]
		public void Initialize()
		{
			_context = new ComponentContext();
			_context.Register(typeof (EmptyComponentAndContract));
		}

		[TestCleanup]
		public void Cleanup()
		{
		}

		#endregion

		[TestMethod]
		public void SimpleGetComponentCall()
		{
			var c = _context.GetComponent<EmptyComponentAndContract>();
			Assert.IsNotNull(c);
		}

		[TestMethod]
		public void TwoCallsForSameObject()
		{
			var c1 = _context.GetComponent<EmptyComponentAndContract>();
			var c2 = _context.GetComponent<EmptyComponentAndContract>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
			Assert.AreSame(c1, c2);
		}

		[TestMethod]
		public void GetComponentWithoutRegistrationReturnsNull()
		{
			_context = new ComponentContext();
			var c = _context.GetComponent<EmptyComponentAndContract>();
			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetAfterUnregisterFamilyReturnsNull()
		{
			_context.UnregisterFamily(typeof(EmptyComponentAndContract));
			var c = _context.GetComponent<EmptyComponentAndContract>();

			Assert.IsNull(c);
		}

		[TestMethod]
		public void GetAfterUnregisterReturnsNull()
		{
			_context.Unregister(new ContractIdentity(typeof(EmptyComponentAndContract), null));
			var c = _context.GetComponent<EmptyComponentAndContract>();

			Assert.IsNull(c);
		}
	}
}