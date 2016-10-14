namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ResourcePlugWithWrongType
	{
		[ResourceManagerPlug("someId")]
		public string WrongTypeResourcePlug { get; set; }
	}
}
