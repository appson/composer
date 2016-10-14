using Compositional.Composer.Interceptor;


namespace Compositional.Composer.Utility.Matching
{
	public class MethodNameCallFilter : ICallFilter
	{
		public string TypeName { get; set; }
		public string MethodName { get; set; }

		public MethodNameCallFilter(string typeName, string methodName)
		{
			TypeName = typeName;
			MethodName = methodName;
		}

		public MethodNameCallFilter()
		{
		}

		#region ICriteria Members

		public bool Match(CallInfo callInfo)
		{
			return TypeName == callInfo.MethodOwner.Name && MethodName == callInfo.MethodName;
		}

		#endregion
	}
}
