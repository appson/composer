using System;
using System.Text.RegularExpressions;

namespace Appson.Composer.Utility.Matching
{
	public class FullNamePatternTypeFilter : ITypeFilter
	{
		private string _pattern;
		private Regex _regex;

		public string Pattern
		{
			get { return _pattern; }
			set
			{
				_pattern = value;
				_regex = new Regex(_pattern);
			}
		}

		#region ITypeFilter implementation

		public bool Match(Type type)
		{
			return _regex != null && _regex.IsMatch(type.FullName);
		}

		#endregion
	}
}