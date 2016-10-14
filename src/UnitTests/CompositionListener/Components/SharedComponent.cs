using Appson.Composer.Cache;

namespace Appson.Composer.UnitTests.CompositionListener.Components
{
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SharedComponent : ISampleContract
	{
	}
}
