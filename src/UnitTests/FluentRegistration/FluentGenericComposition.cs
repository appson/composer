using Appson.Composer.FluentExtensions;
using Appson.Composer.UnitTests.FluentRegistration.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.FluentRegistration
{
    [TestClass]
    public class FluentGenericComposition
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
        public void DirectClosedGeneric()
        {
            _context.ForComponent<ClosedGenericComponent>()
                .RegisterWith<IGenericContract<string>>();

            var c = _context.GetComponent<IGenericContract<string>>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void OpenGeneric()
        {
            Assert.Inconclusive("Fluent configuration for Open Generics are not yet planned");

            _context.ForComponent(typeof(OpenGenericComponent<>))
                .RegisterWith(typeof(IGenericContract<>));

            var c = _context.GetComponent<IGenericContract<string>>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void IncompatibleGenericTypeInContract()
        {
            Assert.Inconclusive("Fluent configuration for Open Generics are not yet planned");

            _context.ForComponent<ClosedGenericComponent>()
                .RegisterWith<IGenericContract<int>>();

            var c = _context.GetComponent<IGenericContract<int>>();

            Assert.IsNotNull(c);
        }

    }
}