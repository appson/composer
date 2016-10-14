using Appson.Composer.UnitTests.CompositionNotification.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appson.Composer.Utility;

namespace Appson.Composer.UnitTests.CompositionNotification
{
	[TestClass]
	public class GetComponentTest
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
			_context.ProcessCompositionXmlFromResource("Appson.Composer.UnitTests.CompositionNotification.Xmls.Composition.xml");
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void ImplicitInterfaceImpl()
		{
			var c = _context.GetComponent<ImplicitInterfaceImpl>();
			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
		}

		[TestMethod]
		public void ExplicitInterfaceImpl()
		{
			var c = _context.GetComponent<ExplicitInterfaceImpl>();
			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
		}

		[TestMethod]
		public void SingleAttributedMethod()
		{
			var c = _context.GetComponent<SingleAttributedMethod>();
			Assert.IsTrue(c.HasAttributedMethodBeenCalled);
		}

		[TestMethod]
		public void MultipleAttributedMethods()
		{
			var c = _context.GetComponent<MultipleAttributedMethods>();
			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledThree);
		}

		[TestMethod]
		public void NonOverlappingAttribAndInterface()
		{
			var c = _context.GetComponent<NonOverlappingAttribAndInterface>();

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);

			Assert.IsFalse(c.HasInterfaceImplBeenCalledTwice);
			Assert.IsTrue(c.HasInterfaceImplBeenCalledAfterAllAttribs);
		}

		[TestMethod]
		public void OverlappingAttribAndInterface()
		{
			var c = _context.GetComponent<OverlappingAttribAndInterface>();

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);

			Assert.IsFalse(c.HasInterfaceImplBeenCalledTwice);
			Assert.IsTrue(c.HasInterfaceImplBeenCalledAfterAllAttribs);
		}
	}
}
