namespace Appson.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class NonContractPlugType
	{
		[ComponentPlug]
		public string WrongPlug { get; set; }
	}
}
