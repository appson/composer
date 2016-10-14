using System.IO;
using System.Resources;

namespace Appson.Composer.Resources
{
	public interface IResourcePointer
	{
		void FillRepository(ResourceRepository repository);

		ResourceManager GetResourceManager();
		Stream GetStream();
	}
}