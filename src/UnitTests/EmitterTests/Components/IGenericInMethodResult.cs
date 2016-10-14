namespace Compositional.Composer.UnitTests.EmitterTests.Components
{
	public interface IGenericInMethodResult<out T>
	{
		T SomeMethod();
	}
}
