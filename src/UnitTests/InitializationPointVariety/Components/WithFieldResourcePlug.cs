using System.Resources;

namespace Appson.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithFieldResourcePlug
	{
		[ResourceManagerPlug("resourceId")]
		public ResourceManager ResourcePlug;
	}
}
