namespace Appson.Composer.UnitTests.CompositionNotification.Components
{
	[Contract]
	[Component]
	public class MultipleAttributedMethods
	{
		public bool HasAttributedMethodBeenCalledOne;
		public bool HasAttributedMethodBeenCalledTwo;
		public bool HasAttributedMethodBeenCalledThree;

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

		[OnCompositionComplete]
		public void NotificationMethodThree()
		{
			HasAttributedMethodBeenCalledThree = true;
		}
	}
}
