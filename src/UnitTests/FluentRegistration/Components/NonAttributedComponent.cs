namespace Appson.Composer.UnitTests.FluentRegistration.Components
{
    public class NonAttributedComponent : INonAttributedContract
    {
        public bool InitCalled;
        public bool InitCalled2;
        public int InitValue;

        public IComponentOne ComponentOne { get; set; }
        public IComponentTwo ComponentTwo { get; set; }

        public string SomeValue { get; set; }
        public int SomeOtherValue { get; set; }

        public NonAttributedComponent()
        {
        }

        public NonAttributedComponent(IComponentOne componentOne, IComponentTwo componentTwo)
        {
            ComponentOne = componentOne;
            ComponentTwo = componentTwo;
        }

        public NonAttributedComponent(string someValue, int someOtherValue)
        {
            SomeValue = someValue;
            SomeOtherValue = someOtherValue;
        }

        public NonAttributedComponent(IComponentOne componentOne, IComponentTwo componentTwo, string someValue, int someOtherValue)
        {
            ComponentOne = componentOne;
            ComponentTwo = componentTwo;
            SomeValue = someValue;
            SomeOtherValue = someOtherValue;
        }

        public void Initialize()
        {
            InitCalled = true;
        }

        public void Initialize2()
        {
            InitCalled2 = true;
        }

        public int ParameterizedInit(int input)
        {
            InitValue = input;
            return input;
        }
    }
}