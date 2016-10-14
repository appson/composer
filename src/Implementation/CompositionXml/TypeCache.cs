using System;
using System.Collections.Generic;
using System.Reflection;
using Appson.Composer.Diagnostics;
using Appson.Composer.Resources;
using Appson.Composer.Utility.Matching;


namespace Appson.Composer.CompositionXml
{
	internal class TypeCache
	{
		private readonly Dictionary<string, Type> _cachedTypeNames;
		private readonly List<string> _namespaceUsings;

		public TypeCache()
		{
			_cachedTypeNames = new Dictionary<string, Type>();
			_namespaceUsings = new List<string>();

			// Import pre-defined assemblies:

			CacheAssembly(typeof(IComposer).Assembly);			// Compositional.Composer
			CacheAssembly(typeof(ComponentContext).Assembly);	// Compositional.Composer.Implementation

			// Add pre-defined namespaces:

			_namespaceUsings.Add(typeof(IComposer).Namespace);			// Compositional.Composer
			_namespaceUsings.Add(typeof(ICallFilter).Namespace);		// Compositional.Composer.Utility.Matching
			_namespaceUsings.Add(typeof(IResourcePointer).Namespace);	// Compositional.Composer.Resources
			_namespaceUsings.Add(typeof(TracingInterceptor).Namespace);	// Compositional.Composer.Diagnostics
		}

		public List<string> NamespaceUsings
		{
			get { return _namespaceUsings; }
		}

		public void CacheAssembly(Assembly assembly)
		{
			var types = assembly.GetExportedTypes();

			foreach (var type in types)
			{
				_cachedTypeNames[type.FullName] = type;
			}
		}

		public Type LookupType(string typeName)
		{
			if (_cachedTypeNames.ContainsKey(typeName))
				return _cachedTypeNames[typeName];

			foreach (var namespaceUsing in _namespaceUsings)
			{
				var fullTypeName = namespaceUsing + "." + typeName;

				if (_cachedTypeNames.ContainsKey(fullTypeName))
					return _cachedTypeNames[fullTypeName];
			}

			return null;
		}
	}
}