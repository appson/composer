namespace Compositional.Composer.UnitTests.Generics.Components
{
	[Contract]
	public interface IGenericContractOne<T>
	{
		T Get();
		void Set(T t);
	}

	[Contract]
	public interface IGenericContractTwo<T1, T2>
	{
		T1 Something(T2 t2);
		T2 AnotherThing(T1 t1);
	}
}