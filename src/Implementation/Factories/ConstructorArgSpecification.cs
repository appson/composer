namespace Compositional.Composer.Factories
{
	public class ConstructorArgSpecification
	{
		public ConstructorArgSpecification(bool required = true, ICompositionalQuery query = null)
		{
			Required = required;
			Query = query;
		}

		public bool Required { get; private set; }
		public ICompositionalQuery Query { get; set; }
	}
}
