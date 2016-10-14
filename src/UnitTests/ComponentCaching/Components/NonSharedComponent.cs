namespace Compositional.Composer.UnitTests.ComponentInstantiations.Components
{
	[Contract]
	[Component]
	[ComponentCache(null)]
	public class NonSharedComponent : ISomeContract, IAnotherContract
	{
	}

	[Contract]
	[Component]
	[ComponentCache(null)]
	public class NonSharedComponentWithPlugs
	{
		[ComponentPlug]
		public SprComponent SprComponent { get; set; }

		[ComponentPlug]
		public SpcComponent SpcComponent { get; set; }

		[ComponentPlug]
		public NonSharedComponent NonSharedComponent { get; set; }
	}
}
