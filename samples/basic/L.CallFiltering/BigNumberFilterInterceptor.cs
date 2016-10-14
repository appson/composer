using System;
using Compositional.Composer.Interceptor;

namespace Compositional.Composer.Samples.Basic
{
	class BigNumberFilterInterceptor : ICallInterceptor
	{
		#region Implementation of ICallInterceptor

		public void BeforeCall(CallInfo callInfo)
		{
			if ((callInfo.MethodOwner == typeof(IAdder)) &&
				(callInfo.MethodName == "Add"))
			{
				var arg0 = (int) callInfo.Arguments[0];
				var arg1 = (int) callInfo.Arguments[1];

				if ((arg0 >= 9) && (arg1 >= 9))
				{
					Console.WriteLine("BLOCKED      - Not authorized to add big numbers.");
					callInfo.ReturnValue = -1;
					callInfo.Completed = true;
				}
				else
				{
					Console.WriteLine("ALLOWED      - Authorized to add small numbers.");
				}
			}
		}

		public void AfterCall(CallInfo callInfo)
		{
			if ((callInfo.MethodOwner == typeof(IMultiplier)) &&
				(callInfo.MethodName == "Multiply"))
			{
				var result = (int) callInfo.ReturnValue;

				if (result > 100)
				{
					Console.WriteLine("BLOCKED      - Result is too large.");
					callInfo.ReturnValue = -1;
				}
			}
		}

		#endregion
	}
}
