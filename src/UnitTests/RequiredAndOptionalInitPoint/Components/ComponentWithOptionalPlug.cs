namespace Compositional.Composer.UnitTests.RequiredAndOptionalInitPoint.Components
{
	[Contract]
	[Component]
	public class ComponentWithOptionalPlug
	{
		[ComponentPlug(false)]
		public IPluggedContract PluggedContract { get; set; }
	}
}
