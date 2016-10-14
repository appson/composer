namespace Appson.Composer.UnitTests.CompositionNotification.Components
{
	[Contract]
	[Component]
	public class NonOverlappingAttribAndInterface : INotifyCompositionCompletion
	{
		public bool HasInterfaceImplBeenCalled;
		public bool HasInterfaceImplBeenCalledTwice;
		public bool HasInterfaceImplBeenCalledAfterAllAttribs;

		public bool HasAttributedMethodBeenCalledOne;
		public bool HasAttributedMethodBeenCalledTwo;

		public void OnCompositionComplete()
		{
			if (HasInterfaceImplBeenCalled)
				HasInterfaceImplBeenCalledTwice = true;

			HasInterfaceImplBeenCalled = true;

			if (HasAttributedMethodBeenCalledOne && HasAttributedMethodBeenCalledTwo)
				HasInterfaceImplBeenCalledAfterAllAttribs = true;
		}

		[OnCompositionComplete]
		public void NotificationMethodOne()
		{
			HasAttributedMethodBeenCalledOne = true;
		}

		[OnCompositionComplete]
		public void NotificationMethodTwo()
		{
			HasAttributedMethodBeenCalledTwo = true;
		}
	}
}
