using System;
using System.Collections.Generic;

namespace Compositional.Composer.Utility
{
	public static class ComposerExtensions
	{
		#region Lazy query utilities

		// GetComponent overloads

		public static Lazy<TContract> LazyGetComponent<TContract>(this IComposer composer) 
			where TContract : class
		{
			return new Lazy<TContract>(composer.GetComponent<TContract>);
		}

		public static Lazy<TContract> LazyGetComponent<TContract>(this IComposer composer, string name) 
			where TContract : class
		{
			return new Lazy<TContract>(() => composer.GetComponent<TContract>(name));
		}

		public static Lazy<object> LazyGetComponent(this IComposer composer, Type contract)
		{
			return new Lazy<object>(() => composer.GetComponent(contract));
		}

		public static Lazy<object> LazyGetComponent(this IComposer composer, Type contract, string name)
		{
			return new Lazy<object>(() => composer.GetComponent(contract, name));
		}

		// GetAllComponents overloads

		public static Lazy<IEnumerable<TContract>> LazyGetAllComponents<TContract>(this IComposer composer) 
			where TContract : class
		{
			return new Lazy<IEnumerable<TContract>>(composer.GetAllComponents<TContract>);
		}

		public static Lazy<IEnumerable<TContract>> LazyGetAllComponents<TContract>(this IComposer composer, string name) 
			where TContract : class
		{
			return new Lazy<IEnumerable<TContract>>(() => composer.GetAllComponents<TContract>(name));
		}

		public static Lazy<IEnumerable<object>> LazyGetAllComponents(this IComposer composer, Type contract)
		{
			return new Lazy<IEnumerable<object>>(() => composer.GetAllComponents(contract));
		}

		public static Lazy<IEnumerable<object>> LazyGetAllComponents(this IComposer composer, Type contract, string name)
		{
			return new Lazy<IEnumerable<object>>(() => composer.GetAllComponents(contract, name));
		}

		// GetComponentFamily overloads

		public static Lazy<IEnumerable<TContract>> LazyGetComponentFamily<TContract>(this IComposer composer)
		{
			return new Lazy<IEnumerable<TContract>>(composer.GetComponentFamily<TContract>);
		}

		public static Lazy<IEnumerable<object>> LazyGetComponentFamily(this IComposer composer, Type contract)
		{
			return new Lazy<IEnumerable<object>>(() => composer.GetComponentFamily(contract));
		}

		// GetVariable overloads

		public static Lazy<object> LazyGetVariable(this IComposer composer, string name)
		{
			return new Lazy<object>(() => composer.GetVariable(name));
		}


		#endregion
	}
}
