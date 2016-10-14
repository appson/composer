namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IMethodWithGenericResult
	{
		T SomeMethod<T>();
	}
}
