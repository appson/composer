using System;
using System.Linq;


namespace Compositional.Composer.Utility.Matching
{
	public class ConjunctionTypeFilter : ITypeFilter
	{
		public ITypeFilter[] Filters { get; set; }

		public ConjunctionTypeFilter(ITypeFilter[] filters)
		{
			Filters = filters;
		}

		public ConjunctionTypeFilter()
		{
		}

		#region ITypeFilter Members

		public bool Match(Type type)
		{
			return Filters.All(filter => filter.Match(type));
		}

		#endregion
	}
}
