using System;
using Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Components;
using Compositional.Composer.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint
{
	[TestClass]
	public class OptionalPlugTest
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
		public void OptPlugProvided()
		{
			_context.Register(typeof(ComponentWithOptionalPlug));
			_context.Register(typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithOptionalPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptPlugProvidedWithDifferentName()
		{
			_context.Register(typeof(ComponentWithOptionalPlug));
			_context.Register("someOtherName", typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithOptionalPlug>();

			Assert.IsNotNull(c);
			Assert.IsNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptPlugNotProvided()
		{
			_context.Register(typeof(ComponentWithOptionalPlug));

			var c = _context.GetComponent<ComponentWithOptionalPlug>();

			Assert.IsNotNull(c);
			Assert.IsNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptNamedPlugProvided()
		{
			_context.Register(typeof(ComponentWithOptionalNamedPlug));
			_context.Register("contractName", typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithOptionalNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptNamedPlugProvidedWithDifferentName()
		{
			_context.Register(typeof(ComponentWithOptionalNamedPlug));
			_context.Register("someOtherName", typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithOptionalNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptNamedPlugProvidedWithoutName()
		{
			_context.Register(typeof(ComponentWithOptionalNamedPlug));
			_context.Register(typeof(PluggedComponent));

			var c = _context.GetComponent<ComponentWithOptionalNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptNamedPlugNotProvided()
		{
			_context.Register(typeof(ComponentWithOptionalNamedPlug));

			var c = _context.GetComponent<ComponentWithOptionalNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNull(c.PluggedContract);
		}

		[TestMethod]
		public void OptNamedPlugRedirectedUponRegistration()
		{
			_context.Register("someOtherName", typeof(PluggedComponent));
			_context.ProcessCompositionXmlFromResource("Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Xmls.OptNamedPlugRedirected.xml");

			var c = _context.GetComponent<ComponentWithOptionalNamedPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.PluggedContract);
		}
	}
}
