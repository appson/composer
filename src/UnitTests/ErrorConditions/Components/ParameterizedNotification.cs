namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ParameterizedNotification
	{
		[OnCompositionComplete]
		public void OnCompositionComplete(string wrongArg)
		{
		}
	}
}
