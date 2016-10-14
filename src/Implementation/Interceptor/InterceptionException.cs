using System;

namespace Appson.Composer.Interceptor
{
	/// <summary>
	/// Encapsulates an exception that has occured during interception of a method call.
	/// </summary>
	public class InterceptionException : Exception
	{
		private readonly bool _afterCall;
		private readonly object[] _arguments;

		private readonly bool _beforeCall;
		private readonly string _methodName;


		public InterceptionException(Exception innerException, string methodName, object[] arguments, bool beforeCall,
		                             bool afterCall)
			: this(
				"An exception is thrown while intercepting a call to " + methodName, innerException, methodName,
				arguments, beforeCall, afterCall)
		{
		}


		public InterceptionException(string message, Exception innerException, string methodName, object[] arguments,
		                             bool beforeCall, bool afterCall)
			: base(message, innerException)
		{
			_methodName = methodName;
			_arguments = arguments;
			_beforeCall = beforeCall;
			_afterCall = afterCall;
		}

		public string MethodName
		{
			get { return _methodName; }
		}

		public object[] Arguments
		{
			get { return _arguments; }
		}

		public bool BeforeCall
		{
			get { return _beforeCall; }
		}

		public bool AfterCall
		{
			get { return _afterCall; }
		}
	}
}