namespace Compositional.Composer.Utility.Matching
{
	public class ExactNameFilter : INameFilter
	{
		private string _methodName;

		public string MethodName
		{
			get { return _methodName; }
			set { _methodName = value; }
		}

		#region INameFilter implementation

		public bool Match(string methodNameToMatch)
		{
			return (methodNameToMatch == _methodName);
		}

		#endregion
	}
}