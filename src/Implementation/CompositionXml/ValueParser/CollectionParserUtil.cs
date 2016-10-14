using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Appson.Composer.CompositionXml.ValueParser
{
	internal static class CollectionParserUtil
	{
		#region Public methods for parsing Arrays and Lists

		/// <summary>
		/// Used when parsing an Array element in composition XML.
		/// </summary>
		public static Array ParseArray(XmlElement element, XmlProcessingContext xmlProcessingContext)
		{
			string elementTypeName = null;

			if (element.Attributes["elementType"] != null)
				elementTypeName = element.Attributes["elementType"].Value;

			var childElements = new List<XmlElement>();
			foreach (XmlNode childNode in element.ChildNodes)
				if (childNode is XmlElement)
					childElements.Add((XmlElement) childNode);

			return ParseArray(elementTypeName, childElements.ToArray(), xmlProcessingContext);
		}

		/// <summary>
		/// Used for parsing short-hand array specification in composition XML.
		/// </summary>
		public static Array ParseArray(IEnumerable<XmlAttribute> attributes, XmlElement[] childElements, XmlProcessingContext context)
		{
			string elementType = null;

			foreach (var attribute in attributes)
			{
				if (attribute.Name == "arrayElementType")
				{
					elementType = attribute.Value;
				}
				else
				{
					context.ReportError(string.Format("Attribute name {0} is not supported for array value elements.", attribute.Name));
					return null;
				}
			}

			if (elementType == null)
			{
				context.ReportError("Attribute 'arrayElementType' is required.");
				return null;
			}

			return ParseArray(elementType, childElements, context);
		}

		/// <summary>
		/// Actually parses an array, and is used in both short-hand and separate
		/// element formats of array specification.
		/// </summary>
		public static Array ParseArray(string elementTypeName, XmlElement[] childElements, XmlProcessingContext context)
		{
			context.EnterRunningLocation(string.Format("Array({0})", elementTypeName ?? "object"));

			var elementType = typeof (object);

			if (elementTypeName != null)
			{
				elementType = SimpleTypeParserUtil.ParseType(elementTypeName, context);
				if (elementType == null)
				{
					context.ReportError(string.Format("Type '{0}' could not be loaded.", elementTypeName));
					return null;
				}
			}

			var arrayElements = GetCollectionElements(childElements, elementType, context);

			var result = Array.CreateInstance(elementType, arrayElements.Count);

			for (var i = 0; i < result.Length; i++)
				result.SetValue(arrayElements[i], i);

			context.LeaveRunningLocation();

			return result;
		}

		/// <summary>
		/// Parses a List element in the composition XML.
		/// </summary>
		public static IList ParseList(XmlElement element, XmlProcessingContext xmlProcessingContext)
		{
			string elementTypeName = null;

			if (element.Attributes["elementType"] != null)
				elementTypeName = element.Attributes["elementType"].Value;

			var childElements = new List<XmlElement>();
			foreach (XmlNode childNode in element.ChildNodes)
				if (childNode is XmlElement)
					childElements.Add((XmlElement) childNode);

			return ParseList(elementTypeName, childElements.ToArray(), xmlProcessingContext);
		}

		/// <summary>
		/// Parses the short-hand form of list specification in composition XML.
		/// </summary>
		public static IList ParseList(IEnumerable<XmlAttribute> attributes, XmlElement[] childElements, XmlProcessingContext context)
		{
			string elementType = null;

			foreach (var attribute in attributes)
			{
				if (attribute.Name == "listElementType")
				{
					elementType = attribute.Value;
				}
				else
				{
					context.ReportError(string.Format("Attribute name {0} is not supported for list value elements.", attribute.Name));
					return null;
				}
			}

			if (elementType == null)
			{
				context.ReportError("Attribute 'listElementType' is required.");
				return null;
			}

			return ParseList(elementType, childElements, context);
		}

		/// <summary>
		/// Actually parses the contents of lists from composition XMLs, and is
		/// used in both short-hand form and complete form of list specification.
		/// </summary>
		public static IList ParseList(string elementTypeName, XmlElement[] childElements, XmlProcessingContext context)
		{
			context.EnterRunningLocation(string.Format("List({0})", elementTypeName ?? "object"));

			var elementType = typeof (object);

			if (elementTypeName != null)
			{
				elementType = SimpleTypeParserUtil.ParseType(elementTypeName, context);
				if (elementType == null)
				{
					context.ReportError(string.Format("Type '{0}' could not be loaded.", elementTypeName));
					context.LeaveRunningLocation();
					return null;
				}
			}

			var listElements = GetCollectionElements(childElements, elementType, context);

			var listType = Type.GetType(string.Format("System.Collections.Generic.List`1[[{0}]]",
			                                          elementType.AssemblyQualifiedName));

			var result = (IList) Activator.CreateInstance(listType);

			foreach (var o in listElements)
				result.Add(o);

			context.LeaveRunningLocation();

			return result;
		}

		#endregion

		#region Public methods for parsing Dictionaries

		/// <summary>
		/// Parses a Dictionary element in the composition XML.
		/// </summary>
		public static IDictionary ParseDictionary(XmlElement element, XmlProcessingContext xmlProcessingContext)
		{
			string keyTypeName = null;
			string valueTypeName = null;

			if (element.Attributes["keyType"] != null)
				keyTypeName = element.Attributes["keyType"].Value;

			if (element.Attributes["valueType"] != null)
				valueTypeName = element.Attributes["valueType"].Value;

			var childElements = new List<XmlElement>();
			foreach (XmlNode childNode in element.ChildNodes)
				if (childNode is XmlElement)
					childElements.Add((XmlElement) childNode);

			return ParseDictionary(keyTypeName, valueTypeName, childElements.ToArray(), xmlProcessingContext);
		}

		/// <summary>
		/// Used for parsing short-hand specification of dictionaries in
		/// the composition XML.
		/// </summary>
		public static IDictionary ParseDictionary(IEnumerable<XmlAttribute> attributes, XmlElement[] childElements,
		                                          XmlProcessingContext context)
		{
			string keyType = null;
			string valueType = null;

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "dictionaryKeyType":
						keyType = attribute.Value;
						break;
					case "dictionaryValueType":
						valueType = attribute.Value;
						break;
					default:
						context.ReportError(string.Format("Attribute name {0} is not supported for array value elements.", attribute.Name));
						return null;
				}
			}

			return ParseDictionary(keyType, valueType, childElements, context);
		}

		/// <summary>
		/// Actually parses the contents of dictionaries in composition XMLs, and is
		/// used for both short-hand and seperate element formats of dictionary specification.
		/// </summary>
		public static IDictionary ParseDictionary(string keyTypeName, string valueTypeName, XmlElement[] xmlElements,
		                                          XmlProcessingContext context)
		{
			context.EnterRunningLocation(
				string.Format("Parse Dictionary({0}, {1})", keyTypeName ?? "object", valueTypeName ?? "object"));

			var keyType = typeof (object);
			var valueType = typeof (object);

			if (keyTypeName != null)
			{
				keyType = SimpleTypeParserUtil.ParseType(keyTypeName, context);
				if (keyType == null)
				{
					context.ReportError(string.Format("Type '{0}' could not be loaded.", keyTypeName));
					context.LeaveRunningLocation();
					return null;
				}
			}

			if (valueTypeName != null)
			{
				valueType = SimpleTypeParserUtil.ParseType(valueTypeName, context);
				if (valueType == null)
				{
					context.ReportError(string.Format("Type '{0}' could not be loaded.", valueTypeName));
					context.LeaveRunningLocation();
					return null;
				}
			}

			var dictionaryType = Type.GetType(string.Format("System.Collections.Generic.Dictionary`2[[{0}],[{1}]]",
			                                                keyType.AssemblyQualifiedName,
			                                                valueType.AssemblyQualifiedName));

			var result = (IDictionary) Activator.CreateInstance(dictionaryType);

			for (var i = 0; i < xmlElements.Length; i++)
			{
				var childElement = xmlElements[i];

				if (childElement.Name == "Item")
				{
					context.EnterRunningLocation(string.Format("Item({0})", i));
					ParseDictionaryItem(result, childElement, context);
					context.LeaveRunningLocation(); // For Item
				}
				else
				{
					context.ReportError(
						string.Format("Xml element '{0}' is not allowed in 'Dictionary' element - type: Dictionary<{1}, {2}>",
						              childElement.Name,
						              keyType.FullName, valueType.FullName));
					context.LeaveRunningLocation();
					return null;
				}
			}

			context.LeaveRunningLocation();
			return result;
		}

		#endregion

		#region Private helper functions

		private static List<object> GetCollectionElements(XmlElement[] xmlElements, Type elementType,
		                                                  XmlProcessingContext xmlProcessingContext)
		{
			var arrayElements = new List<object>();

			for (var i = 0; i < xmlElements.Length; i++)
			{
				xmlProcessingContext.EnterRunningLocation(string.Format("Item({0})", i));

				var childElement = xmlElements[i];

				if (childElement.Name == "Item")
				{
					var arrayElement = XmlValueParser.ParseValue(childElement, xmlProcessingContext, null);

					if ((arrayElement != null) && (!elementType.IsAssignableFrom(arrayElement.GetType())))
					{
						xmlProcessingContext.ReportError(
							string.Format("Array of the element type {0} can not contain items with type {1}.", elementType.FullName,
							              arrayElement.GetType().FullName));
						xmlProcessingContext.LeaveRunningLocation();
						return null;
					}

					arrayElements.Add(arrayElement);
				}
				else
				{
					xmlProcessingContext.ReportError(
						string.Format("Xml element '{0}' is not allowed in 'Array' element - type: {1}[]", childElement.Name,
						              elementType.FullName));
					xmlProcessingContext.LeaveRunningLocation();
					return null;
				}

				xmlProcessingContext.LeaveRunningLocation();
			}

			return arrayElements;
		}

		private static void ParseDictionaryItem(IDictionary result, XmlElement childElement,
		                                        XmlProcessingContext xmlProcessingContext)
		{
			if (childElement.GetElementsByTagName("Key").Count != 1)
			{
				xmlProcessingContext.ReportError(
					"Each 'Item' in a 'Dictionary' should contain exactly one 'Key' element as a nested tag.");
				return;
			}

			if (childElement.GetElementsByTagName("Value").Count != 1)
			{
				xmlProcessingContext.ReportError(
					"Each 'Item' in a 'Dictionary' should contain exactly one 'Value' element as a nested tag.");
				return;
			}

			xmlProcessingContext.EnterRunningLocation("Key");
			var keyValue =
				XmlValueParser.ParseValue((XmlElement) childElement.GetElementsByTagName("Key")[0], xmlProcessingContext, null);
			xmlProcessingContext.LeaveRunningLocation();

			xmlProcessingContext.EnterRunningLocation("Value");
			var valueValue =
				XmlValueParser.ParseValue((XmlElement) childElement.GetElementsByTagName("Value")[0], xmlProcessingContext, null);
			xmlProcessingContext.LeaveRunningLocation();

			result.Add(keyValue, valueValue);
		}

		#endregion
	}
}