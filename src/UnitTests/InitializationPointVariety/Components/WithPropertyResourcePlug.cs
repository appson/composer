using System.Resources;

namespace Appson.Composer.UnitTests.InitializationPointVariety.Components
{
	[Contract]
	[Component]
	public class WithPropertyResourcePlug
	{
		[ResourceManagerPlug("resourceId")]
		public ResourceManager ResourcePlug { get; set; }
	}
}
