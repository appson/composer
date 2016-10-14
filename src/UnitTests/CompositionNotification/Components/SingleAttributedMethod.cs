namespace Compositional.Composer.UnitTests.CompositionNotification.Components
{
	[Contract]
	[Component]
	public class SingleAttributedMethod
	{
		public bool HasAttributedMethodBeenCalled;

		[OnCompositionComplete]
		public void NotificationMethod()
		{
			HasAttributedMethodBeenCalled = true;
		}
	}
}
