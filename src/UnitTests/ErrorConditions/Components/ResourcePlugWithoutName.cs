using System.Resources;

namespace Appson.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ResourcePlugWithoutName
	{
		[ResourceManagerPlug(null)]
		public ResourceManager SomeResource { get; set; }
	}
}
