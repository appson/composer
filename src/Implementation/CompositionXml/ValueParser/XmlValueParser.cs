using System;
using System.Collections.Generic;
using System.Xml;

namespace Appson.Composer.CompositionXml.ValueParser
{
	internal static class XmlValueParser
	{
		public static object ParseValue(XmlElement element, XmlProcessingContext xmlProcessingContext,
		                                List<string> excludedAttributes)
		{
			var xAttributes = new List<XmlAttribute>();
			var xElements = new List<XmlElement>();

			foreach (XmlAttribute xAttribute in element.Attributes)
				xAttributes.Add(xAttribute);

			foreach (XmlNode xNode in element.ChildNodes)
			{
				var xElement = xNode as XmlElement;
				if (xElement == null)
					continue;

				xElements.Add(xElement);
			}

			return ParseValue(xElements.ToArray(), xAttributes.ToArray(), xmlProcessingContext, excludedAttributes);
		}

		public static object ParseValue(XmlElement[] elements, XmlAttribute[] attributes,
		                                XmlProcessingContext xmlProcessingContext, List<string> excludedAttributes)
		{
			if ((excludedAttributes == null) || (excludedAttributes.Count == 0))
				return ParseValue(elements, attributes, xmlProcessingContext);

			var filteredAttributes =
				Array.FindAll(attributes,
				              attribute => !(excludedAttributes.Contains(attribute.Name)));

			return ParseValue(elements, filteredAttributes, xmlProcessingContext);
		}

		public static object ParseValue(XmlElement[] elements, XmlAttribute[] attributes,
		                                XmlProcessingContext xmlProcessingContext)
		{
			// Check if there is any attributes. If there's not,
			// go straight into the nested elements.

			if ((attributes == null) || (attributes.Length == 0))
			{
				// There should be only one element for values that do not
				// specify anythin by the attributes. Otherwise, throw.

				if ((elements == null) || (elements.Length != 1))
				{
					xmlProcessingContext.ReportError("Exactly one nested element should specify the value.");
					return null;
				}

				// If the element is okay, just pass it to the element parser.

				var element = elements[0];

				switch (element.Name)
				{
					case "Object":
						return ObjectParserUtil.ParseObject(element, xmlProcessingContext);

					case "Dictionary":
						return CollectionParserUtil.ParseDictionary(element, xmlProcessingContext);

					case "Array":
						return CollectionParserUtil.ParseArray(element, xmlProcessingContext);

					case "List":
						return CollectionParserUtil.ParseList(element, xmlProcessingContext);
				}

				return SimpleTypeParserUtil.ParseSimpleType(element, xmlProcessingContext);
			}

			// There are attributes. So, based on the name of the attributes,
			// decide who should take responsibility for parsing.

			var attributeNames = new List<string>(
				Array.ConvertAll(attributes, attribute => attribute.Name));

			if (attributeNames.Contains("objectType"))
				return ObjectParserUtil.ParseObject(attributes, elements, xmlProcessingContext);

			if ((attributeNames.Contains("dictionaryKeyType")) || (attributeNames.Contains("dictionaryValueType")))
				return CollectionParserUtil.ParseDictionary(attributes, elements, xmlProcessingContext);

			if (attributeNames.Contains("arrayElementType"))
				return CollectionParserUtil.ParseArray(attributes, elements, xmlProcessingContext);

			return attributeNames.Contains("listElementType")
			       	? CollectionParserUtil.ParseList(attributes, elements, xmlProcessingContext)
			       	: SimpleTypeParserUtil.ParseSimpleType(attributes, xmlProcessingContext);
		}
	}
}