using System;
using System.Collections.Generic;
using System.Linq;


namespace Appson.Composer.CompositionXml
{
	internal class XmlProcessingContext
	{
		private readonly ComponentContext _componentContext;
		private readonly List<XmlProcessingError> _errors;
		private readonly List<string> _runningLocation;
		private readonly TypeCache _typeCache;

		public XmlProcessingContext(ComponentContext componentContext)
		{
			_componentContext = componentContext;
			_typeCache = new TypeCache();
			_runningLocation = new List<string>();
			_errors = new List<XmlProcessingError>();
		}

		public XmlProcessingContext(XmlProcessingContext xmlProcessingContext)
		{
			_componentContext = xmlProcessingContext._componentContext;
			_typeCache = xmlProcessingContext._typeCache;
			_runningLocation = new List<string>();
			_errors = new List<XmlProcessingError>();
		}

		public ComponentContext ComponentContext
		{
			get { return _componentContext; }
		}

		public TypeCache TypeCache
		{
			get { return _typeCache; }
		}

		public List<XmlProcessingError> Errors
		{
			get { return _errors; }
		}

		public string RunningLocationText
		{
			get { return string.Join(" > ", _runningLocation.ToArray()); }
		}

		public void EnterRunningLocation(string locationText)
		{
			if (locationText == null)
				throw new ArgumentNullException("locationText");

			_runningLocation.Add(locationText);
		}

		public void LeaveRunningLocation()
		{
			if (_runningLocation.Count < 1)
				throw new InvalidOperationException("There is no running location to leave!");

			_runningLocation.RemoveAt(_runningLocation.Count - 1);
		}

		public void ReportError(string errorText)
		{
			XmlProcessingError error;

			error.Message = errorText;
			error.RunningLocation = RunningLocationText;

			_errors.Add(error);
		}

		public void ThrowIfErrors()
		{
			if (_errors.Count == 0)
				return;

			var message = _errors.Aggregate("Errors encountered while processing Composition XML file.\r\n\r\n",
			                                (current, error) =>
			                                current +
			                                ("Error text: " + error.Message + "\r\nLocation: " + error.RunningLocation + "\r\n\r\n"));

			throw new CompositionXmlValidationException(message);
		}

		#region Nested type: XmlProcessingError

		public struct XmlProcessingError
		{
			public string Message;
			public string RunningLocation;
		}

		#endregion
	}
}