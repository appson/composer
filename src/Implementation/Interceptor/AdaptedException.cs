using System;

namespace Appson.Composer.Interceptor
{
	/// <summary>
	/// Encapsulates an exception that has occured during interception of a method call.
	/// </summary>
	public class AdaptedException : Exception
	{
		private readonly string _methodName;
		private readonly object[] _arguments;

		private readonly bool _beforeCall;
		private readonly bool _duringCall;
		private readonly bool _afterCall;


		public AdaptedException(Exception innerException, string methodName, object[] arguments,
			bool beforeCall, bool duringCall, bool afterCall)
			: this(
				"An exception is thrown while adapting a call to " + methodName,
				innerException, methodName, arguments, beforeCall, duringCall, afterCall)
		{
		}


		public AdaptedException(string message, Exception innerException, string methodName, object[] arguments,
			bool beforeCall, bool duringCall, bool afterCall)
			: base(message, innerException)
		{
			_methodName = methodName;
			_arguments = arguments;

			_beforeCall = beforeCall;
			_duringCall = duringCall;
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

		public bool DuringCall
		{
			get { return _duringCall; }
		}

		public bool AfterCall
		{
			get { return _afterCall; }
		}
	}
}
