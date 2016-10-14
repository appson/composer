using System.Linq;
using Appson.Composer.Interceptor;


namespace Appson.Composer.Utility.Matching
{
	public class ConjunctionCallFilter : ICallFilter
	{
		public ICallFilter[] Filters { get; set; }

		public ConjunctionCallFilter(ICallFilter[] filters)
		{
			Filters = filters;
		}

		public ConjunctionCallFilter()
		{
		}

		#region ICallFilter Members

		public bool Match(CallInfo callInfo)
		{
			return Filters.All(filter => filter.Match(callInfo));
		}

		#endregion
	}
}
