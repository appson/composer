namespace Appson.Composer.UnitTests.Generics.Components
{
	[Contract]
	[Component]
	public class OpenGenericComponentAndContract<T> : IGenericContractOne<T>
	{
		#region Implementation of IGenericContractOne<T>

		public T Get()
		{
			return default(T);
		}

		public void Set(T t)
		{
			// Do nothing
		}

		#endregion
	}

	[Contract]
	[Component]
	public class HalfOpenComponentAndContract<T> : IGenericContractTwo<string, T>
	{
		#region Implementation of IGenericContractTwo<string,T>

		public string Something(T t2)
		{
			return t2.ToString();
		}

		public T AnotherThing(string t1)
		{
			return default(T);
		}

		#endregion
	}

	[Contract]
	[Component]
	public class ClosedGenericComponentAndContract : IGenericContractOne<string>
	{
		#region Implementation of IGenericContractOne<string>

		public string Get()
		{
			return "";
		}

		public void Set(string t)
		{
			// Do nothing
		}

		#endregion
	}
}