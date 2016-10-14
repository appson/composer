namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IMethodWithGenericRefArg
	{
		void SomeMethod<T>(ref T t);
	}
}
