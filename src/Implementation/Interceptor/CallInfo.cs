using System;
using System.Collections.Generic;
using System.Linq;

namespace Appson.Composer.Interceptor
{
	/// <summary>
	/// Encapsulates all available information when performing a method call,
	/// and all values required when returning a result, either a return value or a thrown exception.
	/// </summary>
	public class CallInfo
	{
		#region Private fields

		private readonly object[] _arguments;
		private readonly Type[] _argumentTypes;
		private readonly Type _methodOwner;
		private readonly string _methodName;
		private readonly Type _resultType;
		private Dictionary<string, object> _additionalInfo;

		#endregion

		#region Constructors

		public CallInfo(Type methodOwner, string methodName, object[] arguments, Type[] argumentTypes, Type resultType)
		{
			_methodOwner = methodOwner;
			_methodName = methodName;
			_arguments = arguments;
			_argumentTypes = argumentTypes;
			_resultType = resultType;
		}

		public CallInfo(CallInfo source)
		{
			_arguments = source._arguments.AsEnumerable().ToArray();
			_argumentTypes = source._argumentTypes.AsEnumerable().ToArray();
			_methodOwner = source._methodOwner;
			_methodName = source._methodName;
			_resultType = source._resultType;
			_additionalInfo = source._additionalInfo == null
			                  	? null
			                  	: new Dictionary<string, object>(source._additionalInfo);

			ReturnValue = source.ReturnValue;
			ThrownException = source.ThrownException;
			Completed = source.Completed;
		}

		#endregion

		#region Properties

		public Type MethodOwner
		{
			get { return _methodOwner; }
		}

		public string MethodName
		{
			get { return _methodName; }
		}

		public object[] Arguments
		{
			get { return _arguments; }
		}

		public Type[] ArgumentTypes
		{
			get { return _argumentTypes; }
		}

		public Type ResultType
		{
			get { return _resultType; }
		}

		public Dictionary<string, object> AdditionalInfo
		{
			get { return _additionalInfo ?? (_additionalInfo = new Dictionary<string, object>()); }
		}

		public object ReturnValue { get; set; }

		public Exception ThrownException { get; set; }

		public bool Completed { get; set; }

		#endregion
	}
}