namespace Compositional.Composer.Samples.Basic
{
	[Contract]
	public interface ICommandPlugin
	{
		string Command { get; }
		string Title { get; }

		void Execute();
	}
}
