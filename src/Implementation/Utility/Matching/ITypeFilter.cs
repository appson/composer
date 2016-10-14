using System;

namespace Appson.Composer.Utility.Matching
{
	public interface ITypeFilter
	{
		bool Match(Type type);
	}

	public delegate bool TypeFilterMatch(Type type);
}