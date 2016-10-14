using Appson.Composer.UnitTests.CompositionByConstructor.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.CompositionByConstructor
{
	[TestClass]
	public class WorkingConstructorLogicTest
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
			_context.Register(typeof(SampleComponentA));
			_context.Register(typeof(SampleComponentB));
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

		[TestMethod]
		public void DefaultConstructorFromMany()
		{
			_context.Register(typeof(MultiConstructorsWithDefault));
			var c = _context.GetComponent<MultiConstructorsWithDefault>();

			Assert.IsNotNull(c);
			Assert.AreEqual(1, c.InvokedConstructor);

			Assert.IsNull(c.A);
			Assert.IsNull(c.B);
		}

		[TestMethod]
		public void MarkedConstructorFromMany()
		{
			_context.Register(typeof(MultiConstructorsWithDefaultAndMarked));
			var c = _context.GetComponent<MultiConstructorsWithDefaultAndMarked>();

			Assert.IsNotNull(c);
			Assert.AreEqual(2, c.InvokedConstructor);

			Assert.IsNotNull(c.A);
			Assert.IsNull(c.B);
		}

		[TestMethod]
		public void SingleConstructor()
		{
			_context.Register(typeof(SingleNonDefaultConstructor));
			var c = _context.GetComponent<SingleNonDefaultConstructor>();

			Assert.IsNotNull(c);
			Assert.AreEqual(3, c.InvokedConstructor);

			Assert.IsNotNull(c.A);
			Assert.IsNotNull(c.B);
		}

		[TestMethod]
		public void NamedConstructorParamsEqual()
		{
			_context.Register(typeof(NamesEqualToParamCount));
			_context.Register("someName", typeof(SampleComponentA));
			_context.Register("someName", typeof(SampleComponentB));

			var c = _context.GetComponent<NamesEqualToParamCount>();

			Assert.IsNotNull(c);

			Assert.IsNotNull(c.DefaultA);
			Assert.IsNotNull(c.DefaultB);
			Assert.IsNotNull(c.NamedA);
			Assert.IsNotNull(c.NamedB);

			Assert.AreNotSame(c.DefaultA, c.NamedA);
			Assert.AreNotSame(c.DefaultB, c.NamedB);
		}

		[TestMethod]
		public void NamedConstructorParamsLess()
		{
			_context.Register(typeof(NamesLessThanParamCount));
			_context.Register("someName", typeof(SampleComponentA));
			_context.Register("someName", typeof(SampleComponentB));

			var c = _context.GetComponent<NamesLessThanParamCount>();

			Assert.IsNotNull(c);

			Assert.IsNotNull(c.DefaultA);
			Assert.IsNotNull(c.DefaultB);
			Assert.IsNotNull(c.NamedA);
			Assert.IsNotNull(c.UnnamedB);

			Assert.AreNotSame(c.DefaultA, c.NamedA);
			Assert.AreSame(c.DefaultB, c.UnnamedB);
		}

	}
}
