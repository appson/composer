using Appson.Composer.FluentExtensions;
using Appson.Composer.UnitTests.FluentRegistration.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.FluentRegistration
{
    [TestClass]
    public class FluentSetConstructorArgsTest
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
            _context.Register(typeof(ComponentOne));
            _context.Register(typeof(IComponentTwo), "name", typeof(ComponentTwo));
            _context.SetVariableValue("one", "someString");
            _context.SetVariableValue("two", 5);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void ConstructorComponent()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorComponent<IComponentOne>()
                .AddConstructorComponent<IComponentTwo>("name")
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.ComponentOne);
            Assert.IsNotNull(c.ComponentTwo);
        }

        [TestMethod]
        public void OptionalConstructorComponent()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorComponent<IComponentOne>(required: false)
                .AddConstructorComponent<IComponentTwo>(required: false)
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.ComponentOne);
            Assert.IsNull(c.ComponentTwo);
        }

        [TestMethod]
        public void ConstructorComponentAsValue()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorValue(new ComponentOne())
                .AddConstructorValue(new ComponentTwo())
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.ComponentOne);
            Assert.IsNotNull(c.ComponentTwo);
        }

        [TestMethod]
        public void ConstructorValue()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorValue("someString")
                .AddConstructorValue(5)
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }

        [TestMethod]
        public void ConstructorValueDelegate()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorValue(cmpsr => cmpsr.GetComponent<IComponentOne>())
                .AddConstructorValue(cmpsr => cmpsr.GetComponent<IComponentTwo>("name"))
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.ComponentOne);
            Assert.IsNotNull(c.ComponentTwo);
        }

        [TestMethod]
        public void ConstructorVariable()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorValueFromVariable("one")
                .AddConstructorValueFromVariable("two")
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }

        [TestMethod]
        public void MixedConstructorArgs()
        {
            _context.ForComponent<NonAttributedComponent>()
                .AddConstructorValue(cmpsr => new ComponentOne())
                .AddConstructorComponent<IComponentTwo>("name")
                .AddConstructorValueFromVariable("one")
                .AddConstructorValue(5)
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.ComponentOne);
            Assert.IsNotNull(c.ComponentTwo);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }


    }
}