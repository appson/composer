namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class NonContractConstructorArg
	{
		public string WrongArg { get; set; }

		[CompositionConstructor]
		public NonContractConstructorArg(string wrongArg)
		{
			WrongArg = wrongArg;
		}
	}
}
