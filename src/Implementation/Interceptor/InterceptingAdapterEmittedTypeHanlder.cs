using System;
using System.Reflection;
using Compositional.Composer.Emitter;

namespace Compositional.Composer.Interceptor
{
	/// <summary>
	/// Acts as a dynamic adapter for any object, by implementing the IEmittedTypeHandler
	/// interface, and redirecting any call to the IEmittedTypeHandler's HandleCall method
	/// to the underlying adapted object. Also, informs an interceptor about the
	/// calls being made, allowing an interceptor to wrap the adapted object.
	/// </summary>
	public class InterceptingAdapterEmittedTypeHanlder : IEmittedTypeHandler
	{
		private readonly object _adapted;
		private readonly Type _adaptedType;
		private readonly ICallInterceptor _interceptor;

		#region Constructors

		/// <summary>
		/// Constructs a new InterceptingAdapterCallHandler object, given an interceptor
		/// and an adapted object.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When creating a new InterceptingAdapterCallHandler, caller can specify only
		/// one adapter, one interceptor, or both of them.
		/// </para>
		/// <para>
		/// If the adapted object is null, the HandleCall method will only pass the method
		/// call information to the interceptor, and ignore the adapted object.
		/// </para>
		/// <para>
		/// If the interceptor is null, the HandleCall method will not intercept the call,
		/// and dispatches the call directly to the adapted object.
		/// </para>
		/// </remarks>
		/// <param name="adapted">
		/// Specifies the target object to be wrapped and recieve
		/// method calls, or null if there is no need to wrap an object.
		/// </param>
		/// <param name="interceptor">
		/// Specifies the interceptor implementation to be notified before and after
		/// each method call.
		/// </param>
		public InterceptingAdapterEmittedTypeHanlder(object adapted, ICallInterceptor interceptor = null)
		{
			if ((adapted == null) && (interceptor == null))
				throw new ArgumentException(
					"Either adapted or interceptor object should be specified for constructing a InterceptingAdapterEmittedTypeHanlder object.");

			_adapted = adapted;
			_interceptor = interceptor;

			if (adapted != null)
				_adaptedType = adapted.GetType();
		}

		/// <summary>
		/// Constructs a new InterceptingAdapterCallHandler without any adapted
		/// object, with the specified interceptor.
		/// </summary>
		/// <param name="interceptor">
		/// Specifies the interceptor implementation to be notified before and after
		/// each method call.
		/// </param>
		public InterceptingAdapterEmittedTypeHanlder(ICallInterceptor interceptor)
			: this(null, interceptor)
		{
		}

		#endregion

		#region ICallHanlder implementation

		public object HandleCall(Type reflectedType, string methodName, object[] arguments, Type[] argumentTypes, Type resultType)
		{
			bool adaptedExceptionBeforeCall = false;
			bool adaptedExceptionDuringCall = false;
			bool adaptedExceptionAfterCall = false;
			Exception previousThrownException = null;

			// Construct the Call Info

			var callInfo = new CallInfo(reflectedType, methodName, arguments, argumentTypes, resultType);

			// Let the intercepter know that the call is being performed

			if (_interceptor != null)
			{
				try
				{
					_interceptor.BeforeCall(callInfo);
				}
				catch (Exception e)
				{
					var message = string.Format(
						"While intercepting a call to '{0}', the BeforeCall method of '{1}' threw an exception of type '{2}' with the following message:\r\n{3}",
						methodName, _interceptor.GetType().FullName, e.GetType().FullName, e.Message);

					throw new InterceptionException(message, e, methodName, arguments, true, false);
				}
			}

			// Check if the interceptor wants the call to throw an exception,
			// store it as an AdaptedException thrown during BeforeCall

			if (callInfo.Completed && callInfo.ThrownException != null)
			{
				adaptedExceptionBeforeCall = true;
				previousThrownException = callInfo.ThrownException;
			}

			// Perform the actual call to the adapted object. The
			// PerformAdaptedCall method should catch any exceptions
			// thrown from the adapted, and put it in the callInfo.

			// Only perform the adapted call if the BeforeCall of the
			// interceptor haven't marked the call as Completed.

			if (!callInfo.Completed)
			{
				PerformAdaptedCall(callInfo);

				if (callInfo.Completed && callInfo.ThrownException != null)
				{
					adaptedExceptionDuringCall = true;
					previousThrownException = callInfo.ThrownException;
				}
			}

			// Inform the interceptor that the method call in finished

			if (_interceptor != null)
			{
				try
				{
					_interceptor.AfterCall(callInfo);
				}
				catch (Exception e)
				{
					var message = string.Format(
						"While intercepting a call to '{0}', the AfterCall method of '{1}' threw an exception of type '{2}' with the following message:\r\n{3}",
						methodName, _interceptor.GetType().FullName, e.GetType().FullName, e.Message);

					throw new InterceptionException(message, e, methodName, arguments, false, true);
				}
			}

			// Check if the AfterCall has marked a ThrownException of its own

			if (callInfo.ThrownException != null)
			{
				if (callInfo.ThrownException != previousThrownException)
				{
					adaptedExceptionAfterCall = true;
				}
			}

			// Decide if the method call should finally throw an exception,
			// or just return the intended value.

			if (callInfo.ThrownException != null)
			{
				var interceptorName = (_interceptor == null)
				                      	? "Null"
				                      	: _interceptor.
				                      	  	GetType().FullName;
				var adaptedName = (_adapted == null)
				                  	? "Null"
				                  	: _adapted.GetType().FullName;

				var message = string.Format(
					"While adapting a call to '{0}' with interceptor '{1}' and adapted type '{2}', an exception of type '{3}' is thrown, with the following message:\r\n{4}",
					methodName, interceptorName, adaptedName, callInfo.ThrownException.GetType().FullName,
					callInfo.ThrownException.Message);

				throw new AdaptedException(message, callInfo.ThrownException, methodName, arguments, adaptedExceptionBeforeCall, adaptedExceptionDuringCall, adaptedExceptionAfterCall);
			}

			return callInfo.ReturnValue;
		}

        public object HandlePropertyGet(Type reflectedType, string propertyName, Type propertyType)
	    {
	        throw new NotImplementedException();
	    }

        public void HandlePropertySet(Type reflectedType, string propertyName, Type propertyType, object newValue)
	    {
	        throw new NotImplementedException();
	    }

        public void HandleEventSubscription(Type reflectedType, string eventName, Type eventType, Delegate target, bool subscribe)
	    {
	        throw new NotImplementedException();
	    }

	    #endregion

		#region Private helper functions

		private void PerformAdaptedCall(CallInfo callInfo)
		{
			callInfo.Completed = true;
			callInfo.ThrownException = null;

			if (_adapted == null)
				return;

			var method = _adaptedType.GetMethod(callInfo.MethodName, callInfo.ArgumentTypes);

			if (method == null)
			{
				var argumentTypesStrings = new string[callInfo.ArgumentTypes.Length];

				for (var i = 0; i < argumentTypesStrings.Length; i++)
					argumentTypesStrings[i] = callInfo.ArgumentTypes[i].ToString();

				throw new ArgumentException(string.Format("The method signature {0}({1}) could not be found in type {2}.",
				                                          callInfo.MethodName, string.Join(", ", argumentTypesStrings),
				                                          _adaptedType.FullName));
			}

			try
			{
				callInfo.ReturnValue = method.Invoke(_adapted, callInfo.Arguments);
			}
			catch (TargetInvocationException e)
			{
				callInfo.ThrownException = e.InnerException;
			}
		}

		#endregion
	}
}