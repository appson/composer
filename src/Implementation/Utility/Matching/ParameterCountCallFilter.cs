using Appson.Composer.Interceptor;


namespace Appson.Composer.Utility.Matching
{
	public class ParameterCountCallFilter : ICallFilter
	{
		public int Count { get; set; }

		public ParameterCountCallFilter(int count)
		{
			Count = count;
		}

		public ParameterCountCallFilter()
		{
		}

		#region ICriteria Members

		public bool Match(CallInfo callInfo)
		{
			return Count == callInfo.Arguments.Length;
		}

		#endregion
	}
}
