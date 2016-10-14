using System;
using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Utility.Matching
{
	public class ParameterTypeCallFilter : ICallFilter
	{
		public int ArgumentIndex { get; set; }
		public Type ParameterType { get; set; }
		public bool ParameterByRef { get; set; }

		public ParameterTypeCallFilter(int argumentIndex, Type parameterType, bool parameterByRef)
		{
			ArgumentIndex = argumentIndex;
			ParameterType = parameterType;
			ParameterByRef = parameterByRef;
		}

		public ParameterTypeCallFilter()
		{
		}

		#region ICriteria Members

		public bool Match(CallInfo callInfo)
		{
			if (ArgumentIndex >= callInfo.ArgumentTypes.Length)
				return false;

			return callInfo.ArgumentTypes[ArgumentIndex].IsAssignableFrom(ParameterType) &&
			       callInfo.ArgumentTypes[ArgumentIndex].IsByRef == ParameterByRef;
		}

		#endregion
	}
}
