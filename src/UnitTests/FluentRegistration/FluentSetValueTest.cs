using Appson.Composer.FluentExtensions;
using Appson.Composer.UnitTests.FluentRegistration.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Appson.Composer.UnitTests.FluentRegistration
{
    [TestClass]
    public class FluentSetValueTest
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
            _context.SetVariableValue("one", "someString");
            _context.SetVariableValue("two", 5);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void SetValue()
        {
            _context.ForComponent<NonAttributedComponent>()
                .SetValue(x => x.SomeValue, "someString")
                .SetValue(x => x.SomeOtherValue, 5)
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }

        [TestMethod]
        public void SetValueFromVariable()
        {
            _context.ForComponent<NonAttributedComponent>()
                .SetValueFromVariable(x => x.SomeValue, "one")
                .SetValueFromVariable(x => x.SomeOtherValue, "two")
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }

        [TestMethod]
        public void SetValueFromDelegate()
        {
            _context.ForComponent<NonAttributedComponent>()
                .SetValue(x => x.SomeValue, cmpsr => "someValue")
                .SetValue(x => x.SomeOtherValue, cmpsr => 5)
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.AreEqual("someString", c.SomeValue);
            Assert.AreEqual(5, c.SomeOtherValue);
        }

        [TestMethod]
        public void SetComponentUsingValueDelegate()
        {
            _context.ForComponent<NonAttributedComponent>()
                .SetValue(x => x.ComponentOne, cmpsr => cmpsr.GetComponent<IComponentOne>())
                .SetValue(x => x.ComponentTwo, cmpsr => cmpsr.GetComponent<IComponentTwo>())
                .RegisterWith<INonAttributedContract>();

            var i = _context.GetComponent<INonAttributedContract>();
            var c = i as NonAttributedComponent;

            Assert.IsNotNull(i);
            Assert.IsNotNull(c);
            Assert.IsNull(c.ComponentOne);
            Assert.IsNull(c.ComponentTwo);
        }
    }
}