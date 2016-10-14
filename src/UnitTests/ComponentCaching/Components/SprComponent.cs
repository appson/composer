using Appson.Composer.Cache;

namespace Appson.Composer.UnitTests.ComponentInstantiations.Components
{
	[Contract]
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SprComponent : ISomeContract, IAnotherContract
	{
	}

	[Contract]
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SprComponentWithPlugs
	{
		[ComponentPlug]
		public SprComponent SprComponent { get; set; }

		[ComponentPlug]
		public SpcComponent SpcComponent { get; set; }

		[ComponentPlug]
		public NonSharedComponent NonSharedComponent { get; set; }
	}
}
