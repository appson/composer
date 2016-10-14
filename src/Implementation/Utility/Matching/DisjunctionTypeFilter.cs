using System;
using System.Linq;


namespace Compositional.Composer.Utility.Matching
{
	public class DisjunctionTypeFilter : ITypeFilter
	{
		public ITypeFilter[] Filters { get; set; }

		public DisjunctionTypeFilter(ITypeFilter[] filters)
		{
			Filters = filters;
		}

		public DisjunctionTypeFilter()
		{
		}

		#region ITypeFilter Members

		public bool Match(Type type)
		{
			return Filters.Any(filter => filter.Match(type));
		}

		#endregion
	}
}
