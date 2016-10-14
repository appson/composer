using System.Linq;
using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Utility.Matching
{
	public class DisjunctionCallFilter : ICallFilter
	{
		public ICallFilter[] Filters { get; set; }

		public DisjunctionCallFilter(ICallFilter[] filters)
		{
			Filters = filters;
		}

		public DisjunctionCallFilter()
		{
		}

		#region ICallFilter Members

		public bool Match(CallInfo callInfo)
		{
			return Filters.Any(filter => filter.Match(callInfo));
		}

		#endregion
	}
}
