using Compositional.Composer.Cache;

namespace Compositional.Composer.UnitTests.CompositionListener.Components
{
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SharedComponent : ISampleContract
	{
	}
}
