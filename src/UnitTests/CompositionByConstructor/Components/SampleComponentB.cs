using Appson.Composer.Cache;

namespace Appson.Composer.UnitTests.CompositionByConstructor.Components
{
	[Component]
	[ComponentCache(typeof(ContractAgnosticComponentCache))]
	public class SampleComponentB : ISampleContractB
	{
	}
}
