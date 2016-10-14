namespace Compositional.Composer.Utility.Matching
{
	public class DelegateNameFilter : INameFilter
	{
		public NameFilterMatch NameFilterMatchMethod { get; set; }

		public DelegateNameFilter(NameFilterMatch method)
		{
			NameFilterMatchMethod = method;
		}

		public DelegateNameFilter()
		{
		}

		#region INameFilter Members

		public bool Match(string name)
		{
			return NameFilterMatchMethod != null ? NameFilterMatchMethod(name) : false;
		}

		#endregion
	}
}
