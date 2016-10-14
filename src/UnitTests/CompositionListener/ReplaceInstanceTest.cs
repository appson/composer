using Compositional.Composer.UnitTests.CompositionListener.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.CompositionListener
{
	[TestClass]
	public class ReplaceInstanceTest
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
			_context.Register(typeof(SampleComponentOne));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void ReplaceWhenCreating()
		{
			var listener = new ReplaceUponCreationListener();
			_context.RegisterCompositionListener("replacer", listener);

			var c = _context.GetComponent<ISampleContract>();

			Assert.IsNotNull(c);
			Assert.IsNotInstanceOfType(c, typeof(SampleComponentOne));
			Assert.IsInstanceOfType(c, typeof(SampleComponentTwo));
		}

		[TestMethod]
		public void ReplaceWhenRetrieving()
		{
			var listener = new ReplaceUponRetrievalListener();
			_context.RegisterCompositionListener("replacer", listener);

			var c = _context.GetComponent<ISampleContract>();

			Assert.IsNotNull(c);
			Assert.IsNotInstanceOfType(c, typeof(SampleComponentOne));
			Assert.IsInstanceOfType(c, typeof(SampleComponentTwo));
		}
	}
}
