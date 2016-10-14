namespace Appson.Composer.UnitTests.Generics.Components
{
	[Contract]
	[Component]
	public class GenericPlug
	{
		[ComponentPlug]
		public IGenericContractOne<string> PlugOne;

		[ComponentPlug]
		public IGenericContractTwo<string, int> PlugTwo;
	}

	[Contract]
	[Component]
	public class IndirectGenericPlug<T>
	{
		[ComponentPlug]
		public IGenericContractOne<T> PlugOne;

		[ComponentPlug]
		public IGenericContractTwo<T, T> PlugTwo;
	}
}