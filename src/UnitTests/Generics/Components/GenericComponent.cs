namespace Compositional.Composer.UnitTests.Generics.Components
{
	[Component]
	public class ClosedComponentOne : IGenericContractOne<string>
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

	[Component]
	public class ClosedComponentTwo : IGenericContractTwo<string, int>
	{
		#region Implementation of IGenericContractTwo<string,int>

		public string Something(int t2)
		{
			return t2.ToString();
		}

		public int AnotherThing(string t1)
		{
			return t1.Length;
		}

		#endregion
	}

	[Component]
	public class OpenComponentOne<T> : IGenericContractOne<T>
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

	[Component]
	public class OpenComponentTwo<T1, T2> : IGenericContractTwo<T1, T2>
	{
		#region Implementation of IGenericContractTwo<T1,T2>

		public T1 Something(T2 t2)
		{
			return default(T1);
		}

		public T2 AnotherThing(T1 t1)
		{
			return default(T2);
		}

		#endregion
	}

	[Component]
	public class HalfOpenComponent<T> : IGenericContractTwo<string, T>
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

	[Component]
	public class RepeatingParamOpenComponent<T> : IGenericContractTwo<T, T>
	{
		#region Implementation of IGenericContractTwo<T,T>

		public T Something(T t2)
		{
			return t2;
		}

		public T AnotherThing(T t1)
		{
			return t1;
		}

		#endregion
	}

	[Component]
	public class ReverseParamOpenComponent<T1, T2> : IGenericContractTwo<T2, T1>
	{
		#region Implementation of IGenericContractTwo<T2,T1>

		public T2 Something(T1 t2)
		{
			return default(T2);
		}

		public T1 AnotherThing(T2 t1)
		{
			return default(T1);
		}

		#endregion
	}
}