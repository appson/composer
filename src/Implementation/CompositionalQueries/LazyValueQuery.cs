using System;

namespace Appson.Composer.CompositionalQueries
{
	public class LazyValueQuery : ICompositionalQuery
	{
		public LazyValueQuery(Lazy<object> value)
		{
		    Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		#region Implementation of ICompositionalQuery

		public object Query(IComposer composer)
		{
			return Value.Value;
		}

		#endregion

		public override string ToString()
		{
			return $"LazyValueQuery('{Value}')";
		}

		public Lazy<object> Value { get; }
	}
}
