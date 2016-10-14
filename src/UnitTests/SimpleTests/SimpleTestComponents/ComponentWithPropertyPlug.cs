namespace Compositional.Composer.UnitTests.SimpleTests.SimpleTestComponents
{
	[Contract]
	[Component]
	public class ComponentWithPropertyPlug
	{
		[ComponentPlug]
		public EmptyComponentAndContract Plug { get; set; }
	}
}