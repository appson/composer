namespace Appson.Composer.Utility.Matching
{
	public interface INameFilter
	{
		bool Match(string name);
	}

	public delegate bool NameFilterMatch(string name);
}