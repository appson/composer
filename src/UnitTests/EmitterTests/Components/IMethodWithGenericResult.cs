namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	public interface IMethodWithGenericResult
	{
		T SomeMethod<T>();
	}
}
