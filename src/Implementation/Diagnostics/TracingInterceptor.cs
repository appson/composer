using System;
using System.Diagnostics;
using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Diagnostics
{
	public class TracingInterceptor : ICallInterceptor
	{
		private readonly string _serviceName;

		private static int _nestingLevel;
		private static bool _shouldCloseCurrentCall;

		public TracingInterceptor(string serviceName)
		{
			_serviceName = serviceName;
		}

		public bool TraceExceptions { get; set; }
		public bool TraceReturnValues { get; set; }
		public bool TraceArgumentCount { get; set; }
		public bool TraceArgumentTypes { get; set; }
		public bool UseFullTypeNames { get; set; }

		#region ICallInterceptor Members

		public void BeforeCall(CallInfo callInfo)
		{
			var traceOutput = new string('|', _nestingLevel);

			traceOutput += "+ ";
			traceOutput += _serviceName;
			traceOutput += ".";
			traceOutput += callInfo.MethodName;

			if (TraceArgumentCount)
			{
				traceOutput += "[";
				traceOutput += callInfo.ArgumentTypes.Length;
				traceOutput += "]";
			}

			if (TraceArgumentTypes)
			{
				traceOutput += "(";

				foreach (var type in callInfo.ArgumentTypes)
				{
					traceOutput += UseFullTypeNames ? type.FullName : type.Name;
					traceOutput += ", ";
				}

				traceOutput += ")";
			}

			_nestingLevel++;
			_shouldCloseCurrentCall = false;

			Trace.WriteLine(traceOutput);
		}

		public void AfterCall(CallInfo callInfo)
		{
			_nestingLevel--;

			if (TraceExceptions && callInfo.ThrownException != null)
			{
				_shouldCloseCurrentCall = true;
				DoTraceExceptions(callInfo.ThrownException);
			}

			if (TraceReturnValues && callInfo.ThrownException == null)
			{
				_shouldCloseCurrentCall = true;
				DoTraceReturnValue(callInfo.ReturnValue);
			}

			if (_shouldCloseCurrentCall)
			{
				DoCloseCurrentCall();
			}

			_shouldCloseCurrentCall = true;
		}

		private void DoTraceExceptions(Exception exception)
		{
			var traceOutput = new string('|', _nestingLevel);

			traceOutput += "   --> THROWN ";
			traceOutput += UseFullTypeNames ? exception.GetType().FullName : exception.GetType().Name;

			Trace.WriteLine(traceOutput);
		}

		private void DoTraceReturnValue(object value)
		{
			var traceOutput = new string('|', _nestingLevel);

			traceOutput += "   --> returned ";

			if (value == null)
			{
				traceOutput += "null";
			}
			else
			{
				traceOutput += UseFullTypeNames ? value.GetType().FullName : value.GetType().Name;
				traceOutput += " (";
				traceOutput += value.ToString();
				traceOutput += ")";
			}

			Trace.WriteLine(traceOutput);
		}

		private static void DoCloseCurrentCall()
		{
			var traceOutput = new string('|', _nestingLevel);

			traceOutput += "\\";

			Trace.WriteLine(traceOutput);
		}

		#endregion
	}
}
