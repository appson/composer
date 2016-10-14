using System.Linq;

namespace Appson.Composer.Utility.Matching
{
	public class ConjunctionNameFilter : INameFilter
	{
		public INameFilter[] Filters { get; set; }

		public ConjunctionNameFilter(INameFilter[] filters)
		{
			Filters = filters;
		}

		public ConjunctionNameFilter()
		{
		}

		#region INameFilter Members

		public bool Match(string name)
		{
			return Filters.All(filter => filter.Match(name));
		}

		#endregion
	}
}
