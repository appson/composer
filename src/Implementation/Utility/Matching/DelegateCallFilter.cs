using Appson.Composer.Interceptor;


namespace Appson.Composer.Utility.Matching
{
	public class DelegateCallFilter : ICallFilter
	{
		public CallFilterMatch CallFilterMatchMethod { get; set; }

		public DelegateCallFilter(CallFilterMatch method)
		{
			CallFilterMatchMethod = method;
		}

		public DelegateCallFilter()
		{
		}

		#region ICallFilter Members

		public bool Match(CallInfo callInfo)
		{
			return CallFilterMatchMethod != null ? CallFilterMatchMethod(callInfo) : false;
		}

		#endregion

	}
}
