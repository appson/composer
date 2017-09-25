namespace Appson.Composer.CompositionalQueries
{
    public class NullQuery : ICompositionalQuery
    {
        public object Query(IComposer composer)
        {
            return null;
        }

        public override string ToString()
        {
            return "NullQuery()";
        }
    }
}