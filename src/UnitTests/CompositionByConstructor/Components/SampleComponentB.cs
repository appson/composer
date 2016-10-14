using Compositional.Composer.Cache;

namespace Compositional.Composer.UnitTests.CompositionByConstructor.Components
{
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SampleComponentB : ISampleContractB
	{
	}
}
