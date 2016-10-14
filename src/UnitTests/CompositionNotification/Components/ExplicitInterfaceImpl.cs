namespace Appson.Composer.UnitTests.CompositionNotification.Components
{
	[Contract]
	[Component]
	public class ExplicitInterfaceImpl : INotifyCompositionCompletion
	{
		public bool HasInterfaceImplBeenCalled;

		void INotifyCompositionCompletion.OnCompositionComplete()
		{
			HasInterfaceImplBeenCalled = true;
		}
	}
}
