using System;
using System.Text.RegularExpressions;

namespace Compositional.Composer.Utility.Matching
{
	public class NamePatternTypeFilter : ITypeFilter
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
			return _regex != null && _regex.IsMatch(type.Name);
		}

		#endregion
	}
}