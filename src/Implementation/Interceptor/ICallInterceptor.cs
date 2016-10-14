namespace Compositional.Composer.Interceptor
{
	/// <summary>
	/// Specifies an interface for call interceptors, where the implementation can be notified
	/// right before and after the actual call.
	/// </summary>
	public interface ICallInterceptor
	{
		void BeforeCall(CallInfo callInfo);
		void AfterCall(CallInfo callInfo);
	}
}