using System;
using Compositional.Composer.Interceptor;

namespace Compositional.Composer.UnitTests.InterceptorTests.Components
{
	internal class RecordingInterceptor : ICallInterceptor
	{
		public CallInfo BeforeCallInfo;
		public CallInfo AfterCallInfo;

		public Action<CallInfo> BeforeCallAction;
		public Action<CallInfo> AfterCallAction;

		#region Implementation of ICallInterceptor

		public void BeforeCall(CallInfo callInfo)
		{
			BeforeCallInfo = new CallInfo(callInfo);

			if (BeforeCallAction != null)
				BeforeCallAction(callInfo);
		}

		public void AfterCall(CallInfo callInfo)
		{
			AfterCallInfo = new CallInfo(callInfo);

			if (AfterCallAction != null)
				AfterCallAction(callInfo);
		}

		#endregion
	}
}
