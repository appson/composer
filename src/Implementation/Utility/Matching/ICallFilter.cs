using Appson.Composer.Interceptor;


namespace Appson.Composer.Utility.Matching
{
	public interface ICallFilter
	{
		bool Match(CallInfo callInfo);
	}

	public delegate bool CallFilterMatch(CallInfo callInfo);
}
