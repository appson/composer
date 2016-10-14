namespace Compositional.Composer
{
	/// <summary>
	/// Provides a means for the Component to be notified of the completion of composition process.
	/// </summary>
	/// <remarks>
	/// Preferred way to be notified when the composition is complete, 
	/// is to use [OnCompositionComplete] attribute on a method, instead of
	/// implementing this interface.
	/// </remarks>
	public interface INotifyCompositionCompletion
	{
		/// <summary>
		/// Is invoked after the composition of the containing component is performed
		/// </summary>
		/// <remarks>
		/// In a Component class that implements INotifyCompositionCompletion, this method is
		/// executed automatically by Composer once the composition of the component is completed. 
		/// (after setting all of the plugs and configuration points)
		/// </remarks>
		void OnCompositionComplete();
	}
}
