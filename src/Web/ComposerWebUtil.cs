using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Appson.Composer.Utility;
using Appson.Composer.Web.Contracts;

namespace Appson.Composer.Web
{
	public static class ComposerWebUtil
	{
		private const string AppKeyComponentContext = "Appson.Composer.Web.ComposerWebUtil.ComponentContext";

		#region Accessing the component context

		// TODO: Change to IComponentContext
		public static ComponentContext ComponentContext
		{
			get => GetComponentContext(HttpContext.Current.Application);
		    private set => SetComponentContext(value, HttpContext.Current.Application);
		}

		public static ComponentContext GetComponentContext(HttpApplicationState application)
		{
			return application[AppKeyComponentContext] as ComponentContext;
		}

		public static void SetComponentContext(ComponentContext componentContext, HttpApplicationState application)
		{
			application[AppKeyComponentContext] = componentContext;
		}

		#endregion

		#region Public Methods

		public static void Setup()
		{
			Setup(new ComponentContext());
		}

		// TODO: Change parameter to IComponentContext
		public static void Setup(ComponentContext composer)
		{
			ComponentContext = composer;

			RegisterDefaultComponents(composer);
			SetResolver(composer);

            composer.ProcessApplicationConfiguration();
		}

		#endregion

		#region Private helper methods

		private static void RegisterDefaultComponents(ComponentContext composer)
		{
			composer.RegisterAssembly(Assembly.GetExecutingAssembly());
		}

		private static void SetResolver(ComponentContext composer)
		{
			var dependencyResolverContract = composer.GetComponent<IDependencyResolverContract>();

			if (dependencyResolverContract != null)
				DependencyResolver.SetResolver(dependencyResolverContract);
		}

		#endregion
	}
}