using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Utility.Matching
{
	public class ArgumentValueCallFilter : ICallFilter
	{
		public int ArgumentIndex { get; set; }
		public object ArgumentValue { get; set; }

		public ArgumentValueCallFilter(int argumentIndex, object argumentValue)
		{
			ArgumentIndex = argumentIndex;
			ArgumentValue = argumentValue;
		}

		public ArgumentValueCallFilter()
		{
		}
        
		#region ICriteria Members

		public bool Match(CallInfo callInfo)
		{
			if (ArgumentIndex >= callInfo.Arguments.Length)
				return false;

			if (ArgumentValue == null)
				return (callInfo.Arguments[ArgumentIndex] == null) ? true : false;

			return ArgumentValue.Equals(callInfo.Arguments[ArgumentIndex]);
		}

		#endregion
	}
}
