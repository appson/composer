using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Microsoft.Win32;

namespace Appson.Composer.CompositionXml.ValueParser
{
	internal static class SimpleTypeParserUtil
	{
		#region General Simple Value parsing method

		public static object ParseSimpleType(XmlAttribute[] attributes, XmlProcessingContext context)
		{
			var attributeNames = new List<string>(
				Array.ConvertAll(attributes, attribute => attribute.Name));

			if (attributeNames.Contains("enumValue"))
				return ParseEnum(attributes, context);
			if (attributeNames.Contains("refType"))
				return ParseRef(attributes, context);
			if (attributeNames.Contains("typeName"))
				return ParseType(attributes, context);
			if (attributeNames.Contains("assemblyName"))
				return ParseAssembly(attributes, context);
			if (attributeNames.Contains("contentsOfVariableName"))
				return ParseContentsOfVariable(attributes, context);
			if (attributeNames.Contains("windowsRegistryKeyPath"))
				return ParseWindowsRegistry(attributes, context);
			if (attributeNames.Contains("timeSpan"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanDays"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanHours"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanMinutes"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanSeconds"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanMilliseconds"))
				return ParseTimeSpan(attributes, context);
			if (attributeNames.Contains("timeSpanTicks"))
				return ParseTimeSpan(attributes, context);

			if (attributes.Length != 1)
			{
				context.ReportError("The simple value is not recognized by Simple Value Parser.");
				return null;
			}

			var attributeName = attributes[0].Name;
			var attributeValue = attributes[0].Value;

			switch (attributeName)
			{
				case "boolean":
					return ParseBoolean(attributeValue);

				case "char":
					return ParseChar(attributeValue);

				case "sByte":
					return ParseSByte(attributeValue);

				case "byte":
					return ParseByte(attributeValue);

				case "int16":
					return ParseInt16(attributeValue);

				case "int32":
					return ParseInt32(attributeValue);

				case "int64":
					return ParseInt64(attributeValue);

				case "uInt16":
					return ParseUInt16(attributeValue);

				case "uInt32":
					return ParseUInt32(attributeValue);

				case "uInt64":
					return ParseUInt64(attributeValue);

				case "single":
					return ParseSingle(attributeValue);

				case "double":
					return ParseDouble(attributeValue);

				case "dateTime":
					return ParseDateTime(attributeValue);

				case "string":
					return ParseString(attributeValue);

				case "byteArray":
					return ParseByteArray(attributeValue, context);

				default:
					context.ReportError("The simple value is not recognized by Simple Value Parser.");
					return null;
			}
		}

		public static object ParseSimpleType(XmlElement element, XmlProcessingContext context)
		{
			var elementName = element.Name;
			var elementInnerText = element.InnerText;


			switch (elementName)
			{
					// Not-so-much simple types

				case "Enum":
					return ParseEnum(element, context);

				case "Ref":
					return ParseRef(element, context);

				case "Type":
					return ParseType(element, context);

				case "Assembly":
					return ParseAssembly(element, context);

				case "ContentsOfVariable":
					return ParseContentsOfVariable(element, context);

				case "WindowsRegistry":
					return ParseWindowsRegistry(element, context);

				case "TimeSpan":
					return ParseTimeSpan(element, context);

				case "SerializeBinary":
					return ParseSerializeBinary(element, context);

					// Very simple types

				case "Boolean":
					return ParseBoolean(elementInnerText);

				case "Char":
					return ParseChar(elementInnerText);

				case "SByte":
					return ParseSByte(elementInnerText);

				case "Byte":
					return ParseByte(elementInnerText);

				case "Int16":
					return ParseInt16(elementInnerText);

				case "Int32":
					return ParseInt32(elementInnerText);

				case "Int64":
					return ParseInt64(elementInnerText);

				case "UInt16":
					return ParseUInt16(elementInnerText);

				case "UInt32":
					return ParseUInt32(elementInnerText);

				case "UInt64":
					return ParseUInt64(elementInnerText);

				case "Single":
					return ParseSingle(elementInnerText);

				case "Double":
					return ParseDouble(elementInnerText);

				case "DateTime":
					return ParseDateTime(elementInnerText);

				case "String":
					return ParseString(elementInnerText);

				case "ByteArray":
					return ParseByteArray(elementInnerText, context);

				case "Null":
					return null;

				case "ComponentContext":
					return context.ComponentContext;

				default:
					context.ReportError("The simple value element is not recognized by Simple Value Parser: " + elementName +
					                    "; Inner text: " + elementInnerText);
					return null;
			}
		}

		#endregion

		#region Very simple type, direct parse methods

		public static Boolean ParseBoolean(string value)
		{
			value = value.Trim();

			if (value.Length == 1)
			{
				if (value[0] == '0') return false;
				if (value[0] == '1') return true;
			}

			return Boolean.Parse(value);
		}

		public static Char ParseChar(string value)
		{
			return Char.Parse(value);
		}

		public static SByte ParseSByte(string value)
		{
			value = value.Trim();
			return SByte.Parse(value);
		}

		public static Byte ParseByte(string value)
		{
			value = value.Trim();
			return Byte.Parse(value);
		}

		public static Int16 ParseInt16(string value)
		{
			value = value.Trim();
			return Int16.Parse(value);
		}

		public static Int32 ParseInt32(string value)
		{
			value = value.Trim();
			return Int32.Parse(value);
		}

		public static Int64 ParseInt64(string value)
		{
			value = value.Trim();
			return Int64.Parse(value);
		}

		public static UInt16 ParseUInt16(string value)
		{
			value = value.Trim();
			return UInt16.Parse(value);
		}

		public static UInt32 ParseUInt32(string value)
		{
			value = value.Trim();
			return UInt32.Parse(value);
		}

		public static UInt64 ParseUInt64(string value)
		{
			value = value.Trim();
			return UInt64.Parse(value);
		}

		public static Single ParseSingle(string value)
		{
			value = value.Trim();
			return Single.Parse(value);
		}

		public static Double ParseDouble(string value)
		{
			value = value.Trim();
			return Double.Parse(value);
		}

		public static DateTime ParseDateTime(string value)
		{
			value = value.Trim();
			return DateTime.Parse(value);
		}

		public static String ParseString(string value)
		{
			return value;
		}

		public static byte[] ParseByteArray(string value, XmlProcessingContext context)
		{
			var filteredString = "";

			// remove all none A-F, 0-9, characters
			for (var i = 0; i < value.Length; i++)
			{
				var c = value[i];

				if ((char.IsWhiteSpace(c)) ||
				    (c == '-') ||
				    (c == '_'))
					continue;

				if (!char.IsLetterOrDigit(c))
				{
					context.ReportError("Character is not allowed in a hex string: '" + c + "', ignoring.");
					continue;
				}

				c = char.ToUpper(c);

				if ((char.IsLetter(c)) && ((c > 'F') || (c < 'A')))
				{
					context.ReportError("Character is not allowed in a hex string: '" + c + "', ignoring.");
					continue;
				}

				filteredString += c;
			}

			// if odd number of characters, add a zero to the beginning
			if (filteredString.Length%2 != 0)
				filteredString = "0" + filteredString;

			var byteLength = filteredString.Length/2;
			var result = new byte[byteLength];

			for (var i = 0; i < result.Length; i++)
				result[i] = byte.Parse(filteredString.Substring(i << 1, 2), NumberStyles.HexNumber);

			return result;
		}

		#endregion

		#region Enum parse methods

		/// <summary>
		/// Parses an Enum element in a composition XML.
		/// </summary>
		public static object ParseEnum(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("Enum elements should not contain any child elements.");
				return null;
			}

			string enumTypeName = null;
			string enumValue = null;

			if (element.Attributes["type"] != null)
				enumTypeName = element.Attributes["type"].Value;

			if (element.Attributes["value"] != null)
				enumValue = element.Attributes["value"].Value;

			return ParseEnum(enumTypeName, enumValue, context);
		}

		/// <summary>
		/// Parses an enum value in its short-hand form in composition XML.
		/// </summary>
		public static object ParseEnum(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string enumType = null;
			string enumValue = null;

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "enumType":
						enumType = attribute.Value;
						break;
					case "enumValue":
						enumValue = attribute.Value;
						break;
					default:
						context.ReportError(
							string.Format("Attribute name {0} is not supported for enum value elements.", attribute.Name));
						return null;
				}
			}

			if ((enumValue == null) || (enumType == null))
			{
				context.ReportError("Both 'enumType' and 'enumValue' attributes are required.");
				return null;
			}

			return ParseEnum(enumType, enumValue, context);
		}

		/// <summary>
		/// Actually parses an Enum specification in the composition XML, and
		/// is used in both element form, and short-hand forms.
		/// </summary>
		public static object ParseEnum(string enumTypeName, string enumValue, XmlProcessingContext context)
		{
			if (enumTypeName == null)
				throw new ArgumentNullException("enumTypeName");

			if (enumValue == null)
				throw new ArgumentNullException("enumValue");

			context.EnterRunningLocation(string.Format("enum({0}, {1})", enumTypeName, enumValue));

			var enumType = ParseType(enumTypeName, context);

			if (enumType == null)
			{
				context.ReportError(string.Format("Type '{0}' could not be loaded.", enumTypeName));
				return null;
			}

			if (!enumType.IsValueType)
			{
				context.ReportError(
					string.Format("Type '{0}' is not an enumeration type, and can not be used in <EnumValue> tag.", enumType));
				return null;
			}

			var result = Enum.Parse(enumType, enumValue);

			context.LeaveRunningLocation();

			return result;
		}

		#endregion

		#region Ref parse methods

		/// <summary>
		/// Parses a Ref element in the composition XML.
		/// </summary>
		public static object ParseRef(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("Ref elements should not contain any child elements.");
				return null;
			}

			string refType = null;
			string refName = null;

			if (element.Attributes["type"] != null)
				refType = element.Attributes["type"].Value;

			if (element.Attributes["name"] != null)
				refName = element.Attributes["name"].Value;

			return ParseRef(refType, refName, context);
		}

		/// <summary>
		/// Parses a Ref specification in the short-hand form from a
		/// composition XML.
		/// </summary>
		public static object ParseRef(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string refType = null;
			string refName = null;

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "refType":
						refType = attribute.Value;
						break;
					case "refName":
						refName = attribute.Value;
						break;
					default:
						context.ReportError(
							string.Format("Attribute name {0} is not supported for ref value elements.", attribute.Name));
						return null;
				}
			}

			if (refType == null)
			{
				context.ReportError("Attribute 'refType' is required.");
				return null;
			}

			return ParseRef(refType, refName, context);
		}

		/// <summary>
		/// Actually parses the contents on Ref specification in composition XML.
		/// Looks up the referenced component in the component context, and returns the result.
		/// Is used both in short-hand form and element form on Ref specification.
		/// </summary>
		public static object ParseRef(string refTypeName, string refName, XmlProcessingContext context)
		{
			if (refTypeName == null)
				throw new ArgumentNullException("refTypeName");

			context.EnterRunningLocation(string.Format("Ref({0})", refTypeName));

			var refType = ParseType(refTypeName, context);

			if (refType == null)
			{
				context.ReportError(string.Format("Type '{0}' could not be loaded.", refTypeName));
				return null;
			}

			var result = context.ComponentContext.GetComponent(refType, refName);

			context.LeaveRunningLocation();

			return result;
		}

		#endregion

		#region Type parse methods

		/// <summary>
		/// Parses a Type element in the composition XML.
		/// </summary>
		public static Type ParseType(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("Type elements should not contain any child elements.");
				return null;
			}

			string typeName = null;

			if (element.Attributes["name"] != null)
				typeName = element.Attributes["name"].Value;

			var result = ParseType(typeName, context);

			if (result == null)
			{
				context.EnterRunningLocation(string.Format("Type({0})", typeName));
				context.ReportError("Could not load type '" + typeName + "'.");
				context.LeaveRunningLocation();
				return null;
			}

			return result;
		}

		/// <summary>
		/// Parses a type specification in composition XML in its short-hand form.
		/// </summary>
		public static Type ParseType(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string typeName = null;

			foreach (var attribute in attributes)
			{
				if (attribute.Name == "typeName")
				{
					typeName = attribute.Value;
				}
				else
				{
					context.ReportError(string.Format("Attribute name {0} is not supported for type value elements.", attribute.Name));
					return null;
				}
			}

			if (typeName == null)
			{
				context.ReportError("Attribute 'typeName' is required.");
				return null;
			}

			var result = ParseType(typeName, context);

			if (result == null)
			{
				context.EnterRunningLocation(string.Format("Type({0})", typeName));
				context.ReportError("Could not load type '" + typeName + "'.");
				context.LeaveRunningLocation();
				return null;
			}


			return result;
		}

		/// <summary>
		/// Actually parses a Type string, looking up the type name. Used in
		/// both short-hand form and element form of type specification.
		/// </summary>
		public static Type ParseType(string typeName, XmlProcessingContext context)
		{
			if (typeName == null)
				throw new ArgumentNullException("typeName");

			var result = context.TypeCache.LookupType(typeName) ?? Type.GetType(typeName, false, false);

			return result;
		}

		#endregion

		#region Assembly parse methods

		/// <summary>
		/// Parses an Assembly element in the composition XML.
		/// </summary>
		public static Assembly ParseAssembly(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("Type elements should not contain any child elements.");
				return null;
			}

			string assemblyName = null;

			if (element.Attributes["name"] != null)
				assemblyName = element.Attributes["name"].Value;

			var result = ParseAssembly(assemblyName, context);

			if (result == null)
			{
				context.EnterRunningLocation(string.Format("Assembly({0})", assemblyName));
				context.ReportError("Could not load assembly '" + assemblyName + "'.");
				context.LeaveRunningLocation();
				return null;
			}

			return result;
		}

		/// <summary>
		/// Parses an assembly specification in composition XML in its short-hand form.
		/// </summary>
		public static Assembly ParseAssembly(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string assemblyName = null;

			foreach (var attribute in attributes)
			{
				if (attribute.Name == "assemblyName")
				{
					assemblyName = attribute.Value;
				}
				else
				{
					context.ReportError(
						string.Format("Attribute name {0} is not supported for assembly value elements.", attribute.Name));
					return null;
				}
			}

			if (assemblyName == null)
			{
				context.ReportError("Attribute 'assemblyName' is required.");
				return null;
			}

			var result = ParseAssembly(assemblyName, context);

			if (result == null)
			{
				context.EnterRunningLocation(string.Format("Assembly({0})", assemblyName));
				context.ReportError("Could not load assembly '" + assemblyName + "'.");
				context.LeaveRunningLocation();
				return null;
			}


			return result;
		}

		/// <summary>
		/// Actually parses an Assembly string, trying to load the assembly object. Used in
		/// both short-hand form and element form of assembly specification.
		/// </summary>
		public static Assembly ParseAssembly(string assemblyName, XmlProcessingContext context)
		{
			if (assemblyName == null)
				throw new ArgumentNullException("assemblyName");

			var result = Assembly.Load(assemblyName);

			return result;
		}

		#endregion

		#region ContentsOfVariable parse methods

		/// <summary>
		/// Parses a ContentsOfVariable element in the composition XML.
		/// </summary>
		public static object ParseContentsOfVariable(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("'ContentsOfVariable' elements should not contain any child elements.");
				return null;
			}

			string variableName = null;

			if (element.Attributes["name"] != null)
				variableName = element.Attributes["name"].Value;

			if (variableName == null)
			{
				context.ReportError("'ContentsOfVariable' elements require a 'name' attribute.");
				return null;
			}

			return ParseContentsOfVariable(variableName, context);
		}

		/// <summary>
		/// Parses a contentsOfVariableName specification in composition XML in its short-hand form.
		/// </summary>
		public static object ParseContentsOfVariable(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string variableName = null;

			foreach (var attribute in attributes)
			{
				if (attribute.Name == "contentsOfVariableName")
				{
					variableName = attribute.Value;
				}
				else
				{
					context.ReportError(
						string.Format("Attribute name {0} is not supported for contentsOfVariable value elements.", attribute.Name));
					return null;
				}
			}

			if (variableName == null)
			{
				context.ReportError("Attribute 'contentsOfVariableName' is required.");
				return null;
			}

			return ParseContentsOfVariable(variableName, context);
		}

		/// <summary>
		/// Actually parses contentsOfVariable string, looking up the variable name. Used in
		/// both short-hand form and element form of contentsOfVariable specification.
		/// </summary>
		public static object ParseContentsOfVariable(string variableName, XmlProcessingContext context)
		{
			if (variableName == null)
				throw new ArgumentNullException("variableName");

			context.EnterRunningLocation(string.Format("Parsing ContentsOfVariable({0})", variableName));

			var result = context.ComponentContext.GetVariable(variableName);

			context.LeaveRunningLocation();

			return result;
		}

		#endregion

		#region WindowsRegistry parse methods

		/// <summary>
		/// Parses a ContentsOfVariable element in the composition XML.
		/// </summary>
		public static object ParseWindowsRegistry(XmlElement element, XmlProcessingContext context)
		{
			if (element.ChildNodes.Count > 0)
			{
				context.ReportError("'WindowsRegistry' elements should not contain any child elements.");
				return null;
			}

			string keyPath = null;
			string valueName = null;

			if (element.Attributes["keyPath"] != null)
				keyPath = element.Attributes["keyPath"].Value;

			if (element.Attributes["valueName"] != null)
				valueName = element.Attributes["valueName"].Value;

			if (keyPath == null)
			{
				context.ReportError("'WindowsRegistry' elements require a 'keyPath' attribute.");
				return null;
			}

			return ParseWindowsRegistry(keyPath, valueName, context);
		}

		/// <summary>
		/// Parses a contentsOfVariableName specification in composition XML in its short-hand form.
		/// </summary>
		public static object ParseWindowsRegistry(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			string keyPath = null;
			string valueName = null;

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "windowsRegistryKeyPath":
						keyPath = attribute.Value;
						break;
					case "windowsRegistryValueName":
						valueName = attribute.Value;
						break;
					default:
						context.ReportError(
							string.Format("Attribute name {0} is not supported for windowsRegistry value elements.", attribute.Name));
						return null;
				}
			}

			if (keyPath == null)
			{
				context.ReportError("Attribute 'windowsRegistryKeyPath' is required.");
				return null;
			}

			return ParseWindowsRegistry(keyPath, valueName, context);
		}

		/// <summary>
		/// Actually parses contentsOfVariable string, looking up the variable name. Used in
		/// both short-hand form and element form of contentsOfVariable specification.
		/// </summary>
		public static object ParseWindowsRegistry(string keyPath, string valueName, XmlProcessingContext context)
		{
			if (keyPath == null)
				throw new ArgumentNullException("keyPath");

			return Registry.GetValue(keyPath, valueName, null);
		}

		#endregion

		#region TimeSpan parse methods

		/// <summary>
		/// Parses a ContentsOfVariable element in the composition XML.
		/// </summary>
		public static object ParseTimeSpan(XmlElement element, XmlProcessingContext context)
		{
			// Inner Text counts as a child node.
			// Check if there is no other child element except the inner text.

			if (element.ChildNodes.Count > 1)
			{
				context.ReportError("'TimeSpan' elements should not contain any child elements.");
				return null;
			}

			if (element.ChildNodes.Count > 0)
			{
				if (!(element.ChildNodes[0] is XmlText))
				{
					context.ReportError("'TimeSpan' elements should not contain any child elements.");
					return null;
				}
			}

			// Declare variables for possible input values

			int? days = null;
			int? hours = null;
			int? minutes = null;
			int? seconds = null;
			int? milliseconds = null;
			long? ticks = null;
			string timeSpanString = null;

			// Extract input values from the attributes (can be in any order)

			if ((element.InnerText != null) && (!string.IsNullOrEmpty(element.InnerText.Trim())))
				timeSpanString = element.InnerText.Trim();

			if (element.Attributes["days"] != null)
				days = Int32.Parse(element.Attributes["days"].Value);

			if (element.Attributes["hours"] != null)
				hours = Int32.Parse(element.Attributes["hours"].Value);

			if (element.Attributes["minutes"] != null)
				minutes = Int32.Parse(element.Attributes["minutes"].Value);

			if (element.Attributes["seconds"] != null)
				seconds = Int32.Parse(element.Attributes["seconds"].Value);

			if (element.Attributes["milliseconds"] != null)
				milliseconds = Int32.Parse(element.Attributes["milliseconds"].Value);

			if (element.Attributes["ticks"] != null)
				ticks = Int64.Parse(element.Attributes["ticks"].Value);

			// Validate and parse the extracted values

			return ParseTimeSpan(timeSpanString, days, hours, minutes, seconds, milliseconds, ticks, context);
		}

		/// <summary>
		/// Parses a contentsOfVariableName specification in composition XML in its short-hand form.
		/// </summary>
		public static object ParseTimeSpan(IEnumerable<XmlAttribute> attributes, XmlProcessingContext context)
		{
			// Declare variables for possible input values
			string timeSpanString = null;
			int? days = null;
			int? hours = null;
			int? minutes = null;
			int? seconds = null;
			int? milliseconds = null;
			long? ticks = null;

			// Extract input values from the attributes (can be in any order)

			foreach (var attribute in attributes)
			{
				switch (attribute.Name)
				{
					case "timeSpan":
						timeSpanString = attribute.Value;
						break;
					case "timeSpanDays":
						days = Int32.Parse(attribute.Value);
						break;
					case "timeSpanHours":
						hours = Int32.Parse(attribute.Value);
						break;
					case "timeSpanMinutes":
						minutes = Int32.Parse(attribute.Value);
						break;
					case "timeSpanSeconds":
						seconds = Int32.Parse(attribute.Value);
						break;
					case "timeSpanMilliseconds":
						milliseconds = Int32.Parse(attribute.Value);
						break;
					case "timeSpanTicks":
						ticks = Int64.Parse(attribute.Value);
						break;
					default:
						context.ReportError(
							string.Format("Attribute name {0} is not supported for timeSpan value elements.", attribute.Name));
						return null;
				}
			}

			// Validate and parse the extracted values

			return ParseTimeSpan(timeSpanString, days, hours, minutes, seconds, milliseconds, ticks, context);
		}

		/// <summary>
		/// Validates the raw values extracted from a TimeSpan element or TimeSpan related
		/// attributes, and then extracts and returns the actual value.
		/// </summary>
		public static object ParseTimeSpan(string timeSpanString, int? days, int? hours, int? minutes, int? seconds,
		                                   int? milliseconds, long? ticks, XmlProcessingContext context)
		{
			object result;
			context.EnterRunningLocation("TimeSpan");

			// Validate the entered values:
			// Should not specify "ticks" with anything else,
			// Should not specify "timeSpanString" with anything else,
			// Should have at least one value specified.

			if (timeSpanString != null)
			{
				if ((ticks.HasValue) ||
				    (days.HasValue) ||
				    (hours.HasValue) ||
				    (minutes.HasValue) ||
				    (seconds.HasValue) ||
				    (milliseconds.HasValue))
				{
					context.ReportError("Cannot specify any other attribute for TimeSpan when the 'string' to be parsed is specified.");
					context.LeaveRunningLocation();
					return null;
				}
			}

			if (ticks.HasValue)
			{
				if ((days.HasValue) ||
				    (hours.HasValue) ||
				    (minutes.HasValue) ||
				    (seconds.HasValue) ||
				    (milliseconds.HasValue))
				{
					context.ReportError("Cannot specify any other attribute for TimeSpan when the ticks is specified.");
					context.LeaveRunningLocation();
					return null;
				}
			}

			if ((timeSpanString == null) &&
			    (!ticks.HasValue) &&
			    (!days.HasValue) &&
			    (!hours.HasValue) &&
			    (!minutes.HasValue) &&
			    (!seconds.HasValue) &&
			    (!milliseconds.HasValue))
			{
				context.ReportError("Should specify at least one of the attributes for the TimeSpan value node.");
				context.LeaveRunningLocation();
				return null;
			}

			// If string is specified, use it to parse the TimeSpan and return.

			if (timeSpanString != null)
			{
				result = ParseTimeSpan(timeSpanString);
				context.LeaveRunningLocation();
				return result;
			}

			// If ticks is specified, use it to parse the TimeSpan and return.

			if (ticks.HasValue)
			{
				result = ParseTimeSpan(ticks.Value);
				context.LeaveRunningLocation();
				return result;
			}

			// Use days/hours/minutes/seconds/milliseconds to create the TimeSpan, and
			// Set every un-specified values to zero, in order to prevent NullReferenceException.

			if (!days.HasValue)
				days = 0;

			if (!hours.HasValue)
				hours = 0;

			if (!minutes.HasValue)
				minutes = 0;

			if (!seconds.HasValue)
				seconds = 0;

			if (!milliseconds.HasValue)
				milliseconds = 0;

			result = ParseTimeSpan(days.Value, hours.Value, minutes.Value, seconds.Value, milliseconds.Value);
			context.LeaveRunningLocation();
			return result;
		}


		/// <summary>
		/// Builds a TimeSpan type based on the supplied days, hours, minutes, seconds,
		/// and milliseconds.
		/// </summary>
		public static object ParseTimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			return new TimeSpan(days, hours, minutes, seconds, milliseconds);
		}

		/// <summary>
		/// Builds a TimeSpan type based on the supplied ticks.
		/// </summary>
		public static object ParseTimeSpan(long ticks)
		{
			return new TimeSpan(ticks);
		}

		/// <summary>
		/// Actually parses timeSpan string, delegating to TimeSpan.Parse static method. Used in
		/// both short-hand form and element form of TimeSpan specification.
		/// </summary>
		public static object ParseTimeSpan(string timeSpanString)
		{
			return TimeSpan.Parse(timeSpanString);
		}

		#endregion

		#region SerializeBinary parse methods

		/// <summary>
		/// Parses a SerializeBinary element in the composition XML.
		/// </summary>
		public static object ParseSerializeBinary(XmlElement element, XmlProcessingContext context)
		{
			context.EnterRunningLocation("SerializeBinary");

			var serializableValue = XmlValueParser.ParseValue(element, context, null);

			var formatter = new BinaryFormatter();
			var stream = new MemoryStream();
			formatter.Serialize(stream, serializableValue);

			var result = stream.ToArray();
			stream.Close();

			context.LeaveRunningLocation();

			return result;
		}

		#endregion

		#region Private helper methods

		#endregion
	}
}