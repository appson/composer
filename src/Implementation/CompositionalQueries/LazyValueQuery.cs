using System;

namespace Appson.Composer.CompositionalQueries
{
	public class LazyValueQuery : ICompositionalQuery
	{
		public LazyValueQuery(Lazy<object> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			Value = value;
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
			return Value.Value;
		}

		#endregion

		public override string ToString()
		{
			return string.Format("Query for a specific pre-defined value: '{0}'", Value);
		}

		public Lazy<object> Value { get; private set; }
	}
}
