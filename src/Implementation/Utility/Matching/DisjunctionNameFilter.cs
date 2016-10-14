using System.Linq;

namespace Compositional.Composer.Utility.Matching
{
	public class DisjunctionNameFilter : INameFilter
	{
		public INameFilter[] Filters { get; set; }

		public DisjunctionNameFilter(INameFilter[] filters)
		{
			Filters = filters;
		}

		public DisjunctionNameFilter()
		{
		}

		#region INameFilter Members

		public bool Match(string name)
		{
			return Filters.Any(filter => filter.Match(name));
		}

		#endregion
	}
}
