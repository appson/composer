namespace Appson.Composer.UnitTests.EmitterTests.Components
{
	public interface IDoubleParamIndexer
	{
		string this[string s, int i] { get; set; }
	}
}
