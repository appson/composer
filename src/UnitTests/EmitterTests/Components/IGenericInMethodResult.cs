namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IGenericInMethodResult<out T>
	{
		T SomeMethod();
	}
}
