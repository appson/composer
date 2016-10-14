using Compositional.Composer.UnitTests.CompositionNotification.Components;
using Compositional.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.CompositionNotification
{
	[TestClass]
	public class InitializePlugsTest
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
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.CompositionNotification.Xmls.Composition.xml");
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void ImplicitInterfaceImpl()
		{
			var c = new ImplicitInterfaceImpl();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
		}

		[TestMethod]
		public void ExplicitInterfaceImpl()
		{
			var c = new ExplicitInterfaceImpl();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
		}

		[TestMethod]
		public void SingleAttributedMethod()
		{
			var c = new SingleAttributedMethod();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasAttributedMethodBeenCalled);
		}

		[TestMethod]
		public void MultipleAttributedMethods()
		{
			var c = new MultipleAttributedMethods();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledThree);
		}

		[TestMethod]
		public void NonOverlappingAttribAndInterface()
		{
			var c = new NonOverlappingAttribAndInterface();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);

			Assert.IsFalse(c.HasInterfaceImplBeenCalledTwice);
			Assert.IsTrue(c.HasInterfaceImplBeenCalledAfterAllAttribs);
		}

		[TestMethod]
		public void OverlappingAttribAndInterface()
		{
			var c = new OverlappingAttribAndInterface();
			_context.InitializePlugs(c);

			Assert.IsTrue(c.HasInterfaceImplBeenCalled);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledOne);
			Assert.IsTrue(c.HasAttributedMethodBeenCalledTwo);

			Assert.IsFalse(c.HasInterfaceImplBeenCalledTwice);
			Assert.IsTrue(c.HasInterfaceImplBeenCalledAfterAllAttribs);
		}
	}
}
