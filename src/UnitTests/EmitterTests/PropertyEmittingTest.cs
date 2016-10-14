using Appson.Composer.UnitTests.EmitterTests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.EmitterTests
{
	[TestClass]
	public class PropertyEmittingTest
	{
		public TestContext TestContext { get; set; }
		private TestingEmittedTypeHandler _dth;

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
			_dth = new TestingEmittedTypeHandler();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion

	}
//* Value-type property, getter only
//* Value-type property, setter only
//* Value-type property, getter and setter
//* Reference-type property, getter only
//* Reference-type property, setter only
//* Reference-type property, getter and setter
//* Property with array of reference-type value
//* Property with array of value-type value
}
