using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Utility.Matching
{
	public interface ICallFilter
	{
		bool Match(CallInfo callInfo);
	}

	public delegate bool CallFilterMatch(CallInfo callInfo);
}
