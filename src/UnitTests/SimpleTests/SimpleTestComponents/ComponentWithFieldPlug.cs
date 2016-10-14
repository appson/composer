namespace Compositional.Composer.UnitTests.SimpleTests.SimpleTestComponents
{
	[Contract]
	[Component]
	public class ComponentWithFieldPlug
	{
		public ComponentWithFieldPlug()
		{
			Plug = null;
		}

		public void SomeMethod()
		{
			Plug = null;
		}

		[ComponentPlug] public EmptyComponentAndContract Plug;
	}
}