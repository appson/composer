using Appson.Composer.UnitTests.RegisterVariety.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.RegisterVariety
{
    [TestClass]
    public class RegisterNonAttributedTest
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
        public void NoAttributesAtAll()
        {
            _context.Configuration.DisableAttributeChecking = true;
            _context.Register(typeof(INonAttributedContractOne), typeof(NonAttributedComponentOne));

            var c = _context.GetComponent<INonAttributedContractOne>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void AttributeOnComponentOnly()
        {
            _context.Configuration.DisableAttributeChecking = true;
            _context.Register(typeof(INonAttributedContractOne), typeof(AttributedComponentOne));

            var c = _context.GetComponent<INonAttributedContractOne>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void SpecifiedAttributeOnContract()
        {
            _context.Configuration.DisableAttributeChecking = true;
            _context.Register(typeof(IContractTwo), typeof(NonAttributedComponentTwo));

            var c = _context.GetComponent<IContractTwo>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void UnspecifiedAttributeOnContract()
        {
            _context.Configuration.DisableAttributeChecking = true;
            _context.Register(typeof(NonAttributedComponentTwo));

            var c = _context.GetComponent<IContractTwo>();

            Assert.IsNotNull(c);
        }
    }
}