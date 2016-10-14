using System;
using System.Runtime.Serialization;

namespace Appson.Composer.CompositionXml
{
	[Serializable]
	public class CompositionXmlValidationException : Exception
	{
		public CompositionXmlValidationException()
		{
		}

		public CompositionXmlValidationException(string message)
			: base(message)
		{
		}

		public CompositionXmlValidationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected CompositionXmlValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}