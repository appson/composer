using System.Resources;

namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ResourcePlugWithoutName
	{
		[ResourceManagerPlug(null)]
		public ResourceManager SomeResource { get; set; }
	}
}
