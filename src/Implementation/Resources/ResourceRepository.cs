using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Appson.Composer.Resources
{
	public class ResourceRepository
	{
		private readonly Dictionary<string, IResourcePointer> _pointers;


		public ResourceRepository()
		{
			_pointers = new Dictionary<string, IResourcePointer>();
		}

		public void Register(string id, IResourcePointer pointer)
		{
			_pointers.Add(id, pointer);
		}

		public bool IsRegistered(string id)
		{
			return _pointers.ContainsKey(id);
		}

		public int Count
		{
			get { return _pointers.Count; }
		}

		public IResourcePointer GetPointer(string id)
		{
			return !_pointers.ContainsKey(id) ? null : _pointers[id];
		}

		public IEnumerable<string> GetIds()
		{
			return _pointers.Keys;
		}

		public IEnumerable<string> FindMatches(Regex regex)
		{
			return _pointers.Keys.Where(id => regex.IsMatch(id));
		}
	}
}