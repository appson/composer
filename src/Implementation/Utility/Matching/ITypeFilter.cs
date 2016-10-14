using System;

namespace Compositional.Composer.Utility.Matching
{
	public interface ITypeFilter
	{
		bool Match(Type type);
	}

	public delegate bool TypeFilterMatch(Type type);
}