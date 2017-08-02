using Appson.Composer.UnitTests.ComponentInstantiations.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.ComponentInstantiations
{
    [TestClass]
    public class ThreadLocalTest
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
            _context.Register(typeof(ThreadLocalComponent));
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void TestGetThreadLocalComponent()
        {
            var component = _context.GetComponent<ISomeContract>();
            Assert.IsNotNull(component);
        }

    }
}