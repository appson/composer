using Appson.Composer.Cache;
using Appson.Composer.FluentExtensions;
using Appson.Composer.UnitTests.FluentRegistration.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.FluentRegistration
{
    [TestClass]
    public class FluentComponentCacheTest
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
            _context.Configuration.DisableAttributeChecking = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void RegisterWithDefaultCache()
        {
            _context.ForComponent<NonAttributedComponent>().RegisterWith<INonAttributedContract>();

            var c1 = _context.GetComponent<INonAttributedContract>();
            var c2 = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.IsTrue(ReferenceEquals(c1, c2));
        }

        [TestMethod]
        public void RegisterWithContractAgnosticCache()
        {
            _context.ForComponent<NonAttributedComponent>()
                .UseComponentCache<ContractAgnosticComponentCache>()
                .RegisterWith<INonAttributedContract>();

            var c1 = _context.GetComponent<INonAttributedContract>();
            var c2 = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.IsTrue(ReferenceEquals(c1, c2));
        }

        [TestMethod]
        public void RegisterWithNoCache()
        {
            _context.ForComponent<NonAttributedComponent>()
                .UseComponentCache(null)
                .RegisterWith<INonAttributedContract>();

            var c1 = _context.GetComponent<INonAttributedContract>();
            var c2 = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.IsFalse(ReferenceEquals(c1, c2));
        }
    }
}