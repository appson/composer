using System;
using System.Linq;
using Compositional.Composer.Interceptor;

namespace Compositional.Composer.Samples.Basic
{
	public class LogToConsoleInterceptor : ICallInterceptor
	{
		#region Implementation of ICallInterceptor

		public void BeforeCall(CallInfo callInfo)
		{
			Console.WriteLine("BEFORE       - {0}.{1}({2})", 
				callInfo.MethodOwner.Name,
				callInfo.MethodName,
				string.Join(", ", callInfo.Arguments.Select(o => o.ToString())));
		}

		public void AfterCall(CallInfo callInfo)
		{
			Console.WriteLine("AFTER        - {0}.{1}({2}) -> {3}",
				callInfo.MethodOwner.Name,
				callInfo.MethodName,
				string.Join(", ", callInfo.Arguments.Select(o => o.ToString())),
				callInfo.ReturnValue ?? "<null>");
		}

		#endregion
	}
}
