using Appson.Composer.FluentExtensions;
using Appson.Composer.UnitTests.FluentRegistration.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.FluentRegistration
{
    [TestClass]
    public class FluentBasicRegistrationTest
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
        public void SimpleRegister()
        {
            _context.ForComponent<NonAttributedComponent>().RegisterWith<INonAttributedContract>();

            var c = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void SimpleRegisterWithoutExplicitContract()
        {
            _context.ForComponent<ComponentOne>().Register();

            var c = _context.GetComponent<IComponentOne>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void SimpleRegisterWithType()
        {
            _context.ForComponent(typeof(NonAttributedComponent)).RegisterWith(typeof(INonAttributedContract));

            var c = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void NamedRegister()
        {
            _context.ForComponent<NonAttributedComponent>().RegisterWith<INonAttributedContract>("name");

            var c = _context.GetComponent<INonAttributedContract>("name");
            Assert.IsNotNull(c);

            var wrongC = _context.GetComponent<INonAttributedContract>();
            Assert.IsNull(wrongC);
        }

        [TestMethod]
        public void RegisterWithSelfType()
        {
            _context.ForComponent<NonAttributedComponent>()
                .RegisterWith<NonAttributedComponent>();

            var c = _context.GetComponent<NonAttributedComponent>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void RegisterObjectWithContract()
        {
            var o = new NonAttributedComponent();

            _context.ForObject(o).RegisterWith<INonAttributedContract>();

            var c = _context.GetComponent<INonAttributedContract>();

            Assert.IsNotNull(c);
            Assert.IsTrue(ReferenceEquals(c, o));
        }

        [TestMethod]
        public void RegisterObjectWithoutExplicitContract()
        {
            var o = new ComponentOne();

            _context.ForObject(o).Register();

            var c = _context.GetComponent<IComponentOne>();

            Assert.IsNotNull(c);
            Assert.IsTrue(ReferenceEquals(c, o));
        }

    }
}