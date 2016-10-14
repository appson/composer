using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;

namespace Compositional.Composer
{
	/// <summary>
	/// Specifies the interface to be used to query for ResourceManagers, when
	/// filling the Resource Manager plugs on components.
	/// </summary>
	[Contract]
	public interface IResourceProvider
	{
		ResourceManager GetResourceManager(string id);
		Stream GetStream(string id);

		IEnumerable<string> GetList();
		IEnumerable<string> GetList(Regex idRegex);
	}
}