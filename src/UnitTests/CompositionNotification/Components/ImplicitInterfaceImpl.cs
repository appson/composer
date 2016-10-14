namespace Appson.Composer.UnitTests.CompositionNotification.Components
{
	[Contract]
	[Component]
	public class ImplicitInterfaceImpl : INotifyCompositionCompletion
	{
		public bool HasInterfaceImplBeenCalled;

		public void OnCompositionComplete()
		{
			HasInterfaceImplBeenCalled = true;
		}
	}
}
