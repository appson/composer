using Appson.Composer.UnitTests.Generics.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.Generics
{
	[TestClass]
	public class OpenGenericsTest
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
		public void ComponentWithOpenGenericParams()
		{
			_context.Register(typeof(OpenComponentOne<>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
			Assert.IsNull(c3);
		}

		[TestMethod]
		public void ComponentWithTwoOpenGenericParams()
		{
			_context.Register(typeof(OpenComponentTwo<,>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNotNull(c3);
		}

		[TestMethod]
		public void ComponentWithHalfOpenGenericParams()
		{
			_context.Register(typeof(HalfOpenComponent<>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();
			var c5 = _context.GetComponent<IGenericContractTwo<string, string>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNotNull(c4);
			Assert.IsNotNull(c5);

			Assert.IsInstanceOfType(c4, typeof(HalfOpenComponent<int>));
			Assert.IsInstanceOfType(c5, typeof(HalfOpenComponent<string>));
		}

		[TestMethod]
		public void ComponentWithRepeatingGenericParams()
		{
			_context.Register(typeof(RepeatingParamOpenComponent<>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();
			var c5 = _context.GetComponent<IGenericContractTwo<string, string>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNull(c4);
			Assert.IsNotNull(c5);

			Assert.IsInstanceOfType(c5, typeof(RepeatingParamOpenComponent<string>));
		}

		[TestMethod]
		public void ComponentWithReverseGenericParams()
		{
			_context.Register(typeof(ReverseParamOpenComponent<,>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();
			var c5 = _context.GetComponent<IGenericContractTwo<string, string>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNotNull(c3);
			Assert.IsNotNull(c4);
			Assert.IsNotNull(c5);

			Assert.IsInstanceOfType(c3, typeof(ReverseParamOpenComponent<string, int>));
			Assert.IsInstanceOfType(c4, typeof(ReverseParamOpenComponent<int, string>));
			Assert.IsInstanceOfType(c5, typeof(ReverseParamOpenComponent<string, string>));
		}

		[TestMethod]
		public void ComponentAndContractWithOpenGenericParams()
		{
			_context.Register(typeof(OpenGenericComponentAndContract<>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();
			var c5 = _context.GetComponent<IGenericContractTwo<string, string>>();
			var c6 = _context.GetComponent<OpenGenericComponentAndContract<string>>();
			var c7 = _context.GetComponent<OpenGenericComponentAndContract<int>>();

			Assert.IsNotNull(c1);
			Assert.IsNotNull(c2);
			Assert.IsNull(c3);
			Assert.IsNull(c4);
			Assert.IsNull(c5);
			Assert.IsNotNull(c6);
			Assert.IsNotNull(c7);

			Assert.IsInstanceOfType(c1, typeof(OpenGenericComponentAndContract<string>));
			Assert.IsInstanceOfType(c2, typeof(OpenGenericComponentAndContract<int>));
			Assert.IsInstanceOfType(c6, typeof(OpenGenericComponentAndContract<string>));
			Assert.IsInstanceOfType(c7, typeof(OpenGenericComponentAndContract<int>));
		}

		[TestMethod]
		public void ComponentAndContractWithHalfOpenGenericParams()
		{
			_context.Register(typeof(HalfOpenComponentAndContract<>));

			var c1 = _context.GetComponent<IGenericContractOne<string>>();
			var c2 = _context.GetComponent<IGenericContractOne<int>>();
			var c3 = _context.GetComponent<IGenericContractTwo<int, string>>();
			var c4 = _context.GetComponent<IGenericContractTwo<string, int>>();
			var c5 = _context.GetComponent<IGenericContractTwo<string, string>>();
			var c6 = _context.GetComponent<HalfOpenComponentAndContract<string>>();
			var c7 = _context.GetComponent<HalfOpenComponentAndContract<int>>();

			Assert.IsNull(c1);
			Assert.IsNull(c2);
			Assert.IsNull(c3);
			Assert.IsNotNull(c4);
			Assert.IsNotNull(c5);
			Assert.IsNotNull(c6);
			Assert.IsNotNull(c7);

			Assert.IsInstanceOfType(c4, typeof(HalfOpenComponentAndContract<int>));
			Assert.IsInstanceOfType(c5, typeof(HalfOpenComponentAndContract<string>));
			Assert.IsInstanceOfType(c6, typeof(HalfOpenComponentAndContract<string>));
			Assert.IsInstanceOfType(c7, typeof(HalfOpenComponentAndContract<int>));
		}

	}
}