using System.Xml;

namespace Compositional.Composer.CompositionXml.ValueParser
{
	internal static class ValidatorUtil
	{
		public static void ValidateXmlValueElement(XmlAttribute[] xAttributes, XmlElement[] xElements, string exceptionMessage)
		{
			if ((xAttributes.Length == 0) && (xElements.Length == 0))
				throw new CompositionXmlValidationException("No value specified for an element that requires a single value: " +
				                                 exceptionMessage);

			if ((xAttributes.Length == 0) && (xElements.Length != 1))
				throw new CompositionXmlValidationException("More than one value specified for an element that requires a single value: " +
				                                 exceptionMessage);
		}
	}
}