namespace Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Components
{
	[Contract]
	[Component]
	public class ComponentWithRequiredPlug
	{
		[ComponentPlug(true)]
		public IPluggedContract PluggedContract { get; set; }
	}
}
