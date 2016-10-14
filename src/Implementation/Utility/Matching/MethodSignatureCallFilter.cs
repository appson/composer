using System;
using System.Linq;
using System.Text;
using Appson.Composer.Interceptor;


namespace Appson.Composer.Utility.Matching
{
	public class MethodSignatureCallFilter : ICallFilter
	{
		public string MethodName { get; set; }

		public Type[] ArgumentTypes { get; set; }

		public MethodSignatureCallFilter()
		{
		}

		public MethodSignatureCallFilter(string methodName, Type[] argumentTypes)
		{
			MethodName = methodName;
			ArgumentTypes = argumentTypes;
		}

		public override string ToString()
		{
			var sb = new StringBuilder(MethodName);

			if (ArgumentTypes != null)
			{
				sb.Append('(');

				for (var i = 0; i < ArgumentTypes.Length; i++)
				{
					sb.Append(ArgumentTypes[i].FullName);
					sb.Append((i == (ArgumentTypes.Length - 1)) ? ")" : ", ");
				}
			}

			return sb.ToString();
		}

		#region ICriteria Members

		public bool Match(CallInfo callInfo)
		{
			if (MethodName == null)
				throw new InvalidOperationException("methodName is null");

			if (ArgumentTypes == null)
				throw new InvalidOperationException("argumentTypes is null");

			if (MethodName != callInfo.MethodName)
				return false;

			if (callInfo.ArgumentTypes.Length != ArgumentTypes.Length)
				return false;

			return
				!ArgumentTypes.Where(
					(t, i) =>
					!t.Equals(callInfo.ArgumentTypes[i].IsByRef
					          	? callInfo.ArgumentTypes[i].GetElementType()
					          	: callInfo.ArgumentTypes[i])).Any();
		}

		#endregion
	}
}
