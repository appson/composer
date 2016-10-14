using Appson.Composer.UnitTests.SimpleTests.SimpleTestComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.SimpleTests
{
	[TestClass]
	public class SimplePlugTest
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
			_context.Register(typeof (ComponentWithFieldPlug));
			_context.Register(typeof (ComponentWithPropertyPlug));
		}

		[TestCleanup]
		public void Cleanup()
		{
		}

		#endregion

		[TestMethod]
		public void GetFieldPlugComponent()
		{
			var c = _context.GetComponent<ComponentWithFieldPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.Plug);
		}

		[TestMethod]
		public void GetPropertyPlugComponent()
		{
			var c = _context.GetComponent<ComponentWithPropertyPlug>();

			Assert.IsNotNull(c);
			Assert.IsNotNull(c.Plug);
		}

		[TestMethod]
		public void TwoComponentUsingOneShared()
		{
			var cField = _context.GetComponent<ComponentWithFieldPlug>();
			var cProperty = _context.GetComponent<ComponentWithPropertyPlug>();

			Assert.AreSame(cField.Plug, cProperty.Plug);
		}

		[TestMethod]
		public void RegistrationOrderInsensitivity()
		{
			var contextOne = new ComponentContext();
			contextOne.Register(typeof(EmptyComponentAndContract));
			contextOne.Register(typeof (ComponentWithFieldPlug));
			contextOne.Register(typeof (ComponentWithPropertyPlug));

			var contextTwo = new ComponentContext();
			contextTwo.Register(typeof (ComponentWithFieldPlug));
			contextTwo.Register(typeof (ComponentWithPropertyPlug));
			contextTwo.Register(typeof(EmptyComponentAndContract));

			var cField = contextOne.GetComponent<ComponentWithFieldPlug>();
			var cProperty = contextOne.GetComponent<ComponentWithPropertyPlug>();
			Assert.IsNotNull(cField);
			Assert.IsNotNull(cProperty);
			Assert.IsNotNull(cField.Plug);
			Assert.IsNotNull(cProperty.Plug);
			Assert.AreSame(cField.Plug, cProperty.Plug);

			cField = contextTwo.GetComponent<ComponentWithFieldPlug>();
			cProperty = contextTwo.GetComponent<ComponentWithPropertyPlug>();
			Assert.IsNotNull(cField);
			Assert.IsNotNull(cProperty);
			Assert.IsNotNull(cField.Plug);
			Assert.IsNotNull(cProperty.Plug);
			Assert.AreSame(cField.Plug, cProperty.Plug);
		}

		[TestMethod]
		[ExpectedException(typeof (CompositionException))]
		public void GetComponentWithoutRegistrationThrows()
		{
			_context = new ComponentContext();
			_context.Register(typeof (ComponentWithFieldPlug));

			_context.GetComponent<ComponentWithFieldPlug>();
		}

		[TestMethod]
		[ExpectedException(typeof (CompositionException))]
		public void GetAfterUnregisterPlugThrows()
		{
			_context.UnregisterFamily(typeof(EmptyComponentAndContract));
			_context.GetComponent<ComponentWithFieldPlug>();
		}
	}
}