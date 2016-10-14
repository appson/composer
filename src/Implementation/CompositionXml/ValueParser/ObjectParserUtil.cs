using System;
using System.Collections.Generic;
using System.Xml;

namespace Appson.Composer.CompositionXml.ValueParser
{
	internal static class ObjectParserUtil
	{
		private static readonly List<string> objectFieldExcludedAttributes;
		private static readonly List<string> objectPropertyExcludedAttributes;

		#region Static constructor

		static ObjectParserUtil()
		{
			objectPropertyExcludedAttributes = new List<string>();
			objectFieldExcludedAttributes = new List<string>();

			objectPropertyExcludedAttributes.Add("name");
			objectFieldExcludedAttributes.Add("name");
		}

		#endregion

		#region Object parse methods

		/// <summary>
		/// Used for parsing an Object element in the composition XML.
		/// </summary>
		public static object ParseObject(XmlElement element, XmlProcessingContext context)
		{
			string typeName = null;
			var initializePlugs = false;

			if (element.Attributes["type"] != null)
				typeName = element.Attributes["type"].Value;

			if (element.Attributes["initializePlugs"] != null)
				initializePlugs = Boolean.Parse(element.Attributes["initializePlugs"].Value);

			if (typeName == null)
			{
				context.ReportError("'Object' element in the composition XML should have a 'type' attribute.");
				return null;
			}

			var childElements = new List<XmlElement>();
			foreach (XmlNode childNode in element.ChildNodes)
				if (childNode is XmlElement)
					childElements.Add((XmlElement) childNode);

			return ParseObject(typeName, initializePlugs, childElements.ToArray(), context);
		}

		/// <summary>
		/// Used for parsing short-hand form of specifying an object in the parent element
		/// </summary>
		public static object ParseObject(XmlAttribute[] attributes, XmlElement[] childElements, XmlProcessingContext context)
		{
			string typeName = null;
			var initializePlugs = false;

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "objectType":
						typeName = attribute.Value;
						break;
					case "objectInitializePlugs":
						initializePlugs = Boolean.Parse(attribute.Value);
						break;
					default:
						context.ReportError(
							string.Format("Attribute name {0} is not supported for object value elements.", attribute.Name));
						return null;
				}
			}

			if (typeName == null)
			{
				context.ReportError("Attribute 'objectType' is required.");
				return null;
			}

			return ParseObject(typeName, initializePlugs, childElements, context);
		}

		/// <summary>
		/// Actually parses the object and returns the result. Used both in short-hand
		/// form and in direct Object element parsing.
		/// </summary>
		public static object ParseObject(string typeName, bool initializePlugs, XmlElement[] childElements,
		                                 XmlProcessingContext context)
		{
			context.EnterRunningLocation(string.Format("Object({0})", typeName));

			// Look for constructor arguments.
			// Set default to null, so that Activator calls default constructor
			// in case the the "ConstructorArgs" element is not specified.

			object[] constructorArguments = null;

			foreach (var childElement in childElements)
			{
				if (childElement.Name != "ConstructorArgs") continue;
				// Check if this is the second "ConstructorArgs" element.
				// If so, report an error and return.

				if (constructorArguments != null)
				{
					context.ReportError("The 'ConstructorArgs' element can be specified maximum once in an 'Object' element.");
					context.LeaveRunningLocation();
					return null;
				}

				constructorArguments = ParseConstructorArgs(childElement, context);
				context.ThrowIfErrors();
			}

			// Resolve the "Type" for the object to be instantiated			

			var objectType = SimpleTypeParserUtil.ParseType(typeName, context);

			if (objectType == null)
			{
				context.ReportError(string.Format("Type '{0}' could not be loaded.", typeName));
				context.LeaveRunningLocation();
				return null;
			}

			// Use Activator class to instantiate the object

			var result = Activator.CreateInstance(objectType, constructorArguments);

			if (initializePlugs)
				context.ComponentContext.InitializePlugs(result, objectType);

			foreach (var childElement in childElements)
			{
				switch (childElement.Name)
				{
					case "ConstructorArgs":
						break;

					case "Property":
						ParseObjectProperty(result, childElement, context);
						break;
					
					case "Field":
						ParseObjectField(result, childElement, context);
						break;

					default: 	// Also: case "ConstructorArgs"
						context.ReportError(
							string.Format("Xml element '{0}' is not allowed in 'Object' element - type: {1}", childElement.Name,
							              typeName));
						context.LeaveRunningLocation();
						return null;
				}
			}

			context.LeaveRunningLocation();
			return result;
		}

		#endregion

		#region Private helper methods

		private static void ParseObjectProperty(object result, XmlElement childElement,
		                                        XmlProcessingContext xmlProcessingContext)
		{
			if (childElement.Attributes["name"] == null)
			{
				xmlProcessingContext.ReportError("'Property' element in the composition XML should have a 'name' attribute.");
				return;
			}

			var propertyName = childElement.Attributes["name"].Value;

			xmlProcessingContext.EnterRunningLocation(string.Format("Property({0})", propertyName));

			var propertyInfo = result.GetType().GetProperty(propertyName);
			if (propertyInfo == null)
			{
				xmlProcessingContext.ReportError(
					string.Format("Object type '{0}' does not contain a property definition named '{1}", result.GetType().FullName,
					              propertyName));
				xmlProcessingContext.LeaveRunningLocation();
				return;
			}

			var propertyValue =
				XmlValueParser.ParseValue(childElement, xmlProcessingContext, objectPropertyExcludedAttributes);
			propertyInfo.SetValue(result, propertyValue, null);

			xmlProcessingContext.LeaveRunningLocation();
		}

		private static void ParseObjectField(object result, XmlElement childElement, XmlProcessingContext xmlProcessingContext)
		{
			if (childElement.Attributes["name"] == null)
			{
				xmlProcessingContext.ReportError("'Field' element in the composition XML should have a 'name' attribute.");
				return;
			}

			var fieldName = childElement.Attributes["name"].Value;

			xmlProcessingContext.EnterRunningLocation(string.Format("Field({0})", fieldName));

			var fieldInfo = result.GetType().GetField(fieldName);
			if (fieldInfo == null)
			{
				xmlProcessingContext.ReportError(
					string.Format("Object type '{0}' does not contain a field definition named '{1}", result.GetType().FullName,
					              fieldName));
				xmlProcessingContext.LeaveRunningLocation();
				return;
			}

			var fieldValue = XmlValueParser.ParseValue(childElement, xmlProcessingContext, objectFieldExcludedAttributes);
			fieldInfo.SetValue(result, fieldValue);

			xmlProcessingContext.LeaveRunningLocation();
		}

		private static object[] ParseConstructorArgs(XmlElement element,
		                                             XmlProcessingContext xmlProcessingContext)
		{
			if (element.Name != "ConstructorArgs")
				throw new ArgumentException("Calling this method is only valid for 'ConstructorArgs' element.");

			if (element.HasAttributes)
				xmlProcessingContext.ReportError("'ConstructorArgs' element should not have any attributes.");

			xmlProcessingContext.EnterRunningLocation("ConstructorArgs");

			var result = new List<object>();

			foreach (XmlNode childNode in element.ChildNodes)
			{
				var childElement = childNode as XmlElement;
				if (childElement == null)
					continue;

				if (childElement.Name == "Arg")
					result.Add(XmlValueParser.ParseValue(childElement, xmlProcessingContext, null));
				else
					xmlProcessingContext.ReportError(
						string.Format("Element '{0}' is not allowed in the 'ConstructorArgs' element.", childElement.Name));
			}

			xmlProcessingContext.LeaveRunningLocation();
			return result.ToArray();
		}

		#endregion
	}
}