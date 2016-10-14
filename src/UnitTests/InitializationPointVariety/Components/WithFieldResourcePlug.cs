using System.Resources;

namespace Compositional.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithFieldResourcePlug
	{
		[ResourceManagerPlug("resourceId")]
		public ResourceManager ResourcePlug;
	}
}
