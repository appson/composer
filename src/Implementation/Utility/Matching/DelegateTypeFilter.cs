using System;


namespace Appson.Composer.Utility.Matching
{
	public class DelegateTypeFilter : ITypeFilter
	{
		public TypeFilterMatch TypeFilterMatchMethod { get; set; }

		public DelegateTypeFilter(TypeFilterMatch method)
		{
			TypeFilterMatchMethod = method;
		}

		public DelegateTypeFilter()
		{
		}

		#region ITypeFilter Members

		public bool Match(Type type)
		{
			return TypeFilterMatchMethod != null ? TypeFilterMatchMethod(type) : false;
		}

		#endregion
	}
}
