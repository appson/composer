using System;
using Appson.Composer.Cache;

namespace Appson.Composer
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class ComponentCacheAttribute : Attribute
	{
		private readonly Type _componentCacheType;
		private readonly string _componentCacheName;

		public ComponentCacheAttribute()
			: this(typeof(DefaultComponentCache), null)
		{
		}

		public ComponentCacheAttribute(Type componentCacheType)
			: this(componentCacheType, null)
		{
		}

		public ComponentCacheAttribute(Type componentCacheType, string componentCacheName)
		{
			_componentCacheType = componentCacheType;
			_componentCacheName = componentCacheName;
		}

		public Type ComponentCacheType
		{
			get { return _componentCacheType; }
		}

		public string ComponentCacheName
		{
			get { return _componentCacheName; }
		}
	}
}