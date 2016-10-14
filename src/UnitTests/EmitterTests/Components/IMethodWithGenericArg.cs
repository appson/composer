namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	public interface IMethodWithGenericArg
	{
		void SomeMethod<T>(T t);
	}
}
