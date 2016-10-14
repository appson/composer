using Compositional.Composer.Diagnostics;
using Compositional.Composer.UnitTests.CompositionListener.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.CompositionListener
{
	[TestClass]
	public class CountEventsTest
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
		public void CountEventsGetShared()
		{
			_context.Register(typeof(SharedComponent));

			var listener = new CountingCompositionListener();
			_context.RegisterCompositionListener("counter", listener);

			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();

			Assert.AreEqual(1, listener.OnComponentCreatedCount);
			Assert.AreEqual(1, listener.OnComponentComposedCount);
			Assert.AreEqual(3, listener.OnComponentRetrievedCount);
		}

		[TestMethod]
		public void CountEventsGetNonShared()
		{
			_context.Register(typeof(NonSharedComponent));

			var listener = new CountingCompositionListener();
			_context.RegisterCompositionListener("counter", listener);

			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();
			_context.GetComponent<ISampleContract>();

			Assert.AreEqual(3, listener.OnComponentCreatedCount);
			Assert.AreEqual(3, listener.OnComponentComposedCount);
			Assert.AreEqual(3, listener.OnComponentRetrievedCount);
		}

		[TestMethod]
		public void CountEventsInitializePlugs()
		{
			var listener = new CountingCompositionListener();
			_context.RegisterCompositionListener("counter", listener);

			_context.InitializePlugs(new SharedComponent());
			_context.InitializePlugs(new SharedComponent());
			_context.InitializePlugs(new SharedComponent());

			Assert.AreEqual(0, listener.OnComponentCreatedCount);
			Assert.AreEqual(3, listener.OnComponentComposedCount);
			Assert.AreEqual(0, listener.OnComponentRetrievedCount);
		}
	}
}
