using Appson.Composer.UnitTests.Generics.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.Generics
{
	[TestClass]
	public class ClosedGenericsTest
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
		public void ComponentWithClosedGenericParams()
		{
			_context.Register(typeof(ClosedComponentOne));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
		}

		[TestMethod]
		public void ComponentWithTwoClosedGenericParams()
		{
			_context.Register(typeof(ClosedComponentTwo));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNotNull(c4);
		}

		[TestMethod]
		public void ComponentContractWithClosedGenericParam()
		{
			_context.Register(typeof(ClosedGenericComponentAndContract));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<ClosedGenericComponentAndContract>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNotNull(c4);
		}

		[TestMethod]
		public void CloseAnOpenGenericComponentWhenRegistering()
		{
			_context.Register(typeof(OpenGenericComponentAndContract<string>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<OpenGenericComponentAndContract<string>>();

			Assert.IsNotNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNotNull(c4);
		}
	}
}