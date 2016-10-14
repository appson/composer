using Compositional.Composer.Utility;
using Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint
{
	[TestClass]
	public class RequiredPlugTest
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
		public void ReqPlugProvided()
		{
			_context.Register(typeof(ComponentWithRequiredPlug));
			_context.Register(typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithRequiredPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqPlugProvidedWithDifferentName()
		{
			_context.Register(typeof(ComponentWithRequiredPlug));
			_context.Register("someOtherName", typeof(PluggedComponent));

			_context.GetComponent<ComponentWithRequiredPlug>();
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqPlugNotProvided()
		{
			_context.Register(typeof(ComponentWithRequiredPlug));

			_context.GetComponent<ComponentWithRequiredPlug>();
		}

		[TestMethod]
		public void ReqNamedPlugProvided()
		{
			_context.Register(typeof(ComponentWithRequiredNamedPlug));
			_context.Register("contractName", typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithRequiredNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqNamedPlugProvidedWithDifferentName()
		{
			_context.Register(typeof(ComponentWithRequiredNamedPlug));
			_context.Register("someOtherName", typeof(PluggedComponent));

			_context.GetComponent<ComponentWithRequiredNamedPlug>();
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqNamedPlugProvidedWithoutName()
		{
			_context.Register(typeof(ComponentWithRequiredNamedPlug));
			_context.Register(typeof(PluggedComponent));

			_context.GetComponent<ComponentWithRequiredNamedPlug>();
		}

		[TestMethod]
		[ExpectedException(typeof(CompositionException))]
		public void ReqNamedPlugNotProvided()
		{
			_context.Register(typeof(ComponentWithRequiredNamedPlug));

			_context.GetComponent<ComponentWithRequiredNamedPlug>();
		}

		[TestMethod]
		public void ReqNamedPlugRedirectedUponRegistration()
		{
			_context.Register("someOtherName", typeof(PluggedComponent));
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Xmls.ReqNamedPlugRedirected.xml");

			var c = _context.GetComponent<ComponentWithRequiredNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}
	}
}
