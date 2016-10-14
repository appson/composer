namespace Compositional.Composer.UnitTests.XmlValueParser.Components
{
	public class SampleClass
	{
		[ComponentPlug]
		public ISampleContract SampleContract { get; set; }

		public string ConstructorArg { get; private set; }

		public string Property { get; set; }

		public string Field;

		public SampleClass()
		{
			Field = null;
		}

		public SampleClass(string constructorArg)
		{
			ConstructorArg = constructorArg;
		}
	}
}
