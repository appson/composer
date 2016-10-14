using Compositional.Composer.Cache;

namespace Compositional.Composer.UnitTests.ComponentInstantiations.Components
{
	[Contract]
	[Component]
	[ComponentCache(typeof(DefaultComponentCache))]
	public class SpcComponent : ISomeContract, IAnotherContract
	{
	}

	[Contract]
	[Component]
	[ComponentCache(typeof(DefaultComponentCache))]
	public class SpcComponentWithPlugs
	{
		[ComponentPlug]
		public SprComponent SprComponent { get; set; }

		[ComponentPlug]
		public SpcComponent SpcComponent { get; set; }

		[ComponentPlug]
		public NonSharedComponent NonSharedComponent { get; set; }
	}
}
