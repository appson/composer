namespace Compositional.Composer.Samples.Basic
{
    /// <summary>
    /// The contract that is supposed to be provided by the main
    /// program. In other words, instead of writing the main
    /// program code in Main method, the program code should be
    /// placed in a component providing the following contract.
    /// </summary>
    [Contract]
	public interface IProgramRunner
	{
		/// <summary>
		/// Called when the program starts.
		/// </summary>
        void Run();
	}
}
