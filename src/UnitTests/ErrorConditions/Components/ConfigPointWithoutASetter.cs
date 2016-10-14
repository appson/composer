namespace Compositional.Composer.UnitTests.ErrorConditions.Components
{
	[Contract]
	[Component]
	public class ConfigPointWithoutSetter
	{
		private readonly string _someConfig;

		public ConfigPointWithoutSetter()
		{
			_someConfig = null;
		}

		[ConfigurationPoint("someVarName")]
		public string SomeConfig
		{
			get
			{
				return _someConfig;
			}
		}
	}
}
