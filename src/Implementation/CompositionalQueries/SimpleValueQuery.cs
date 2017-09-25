namespace Appson.Composer.CompositionalQueries
{
    public class SimpleValueQuery : ICompositionalQuery
    {
        public SimpleValueQuery(object value)
        {
            Value = value;
        }

        #region Implementation of ICompositionalQuery

        public object Query(IComposer composer)
        {
            return Value;
        }

        #endregion

        public override string ToString()
        {
            return $"SimpleValueQuery('{Value}')";
        }

        public object Value { get; }
    }
}