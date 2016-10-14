namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IMethodWithOptionalArg
	{
		void SomeMethod(string s = "default", int i = 1);
	}
}
