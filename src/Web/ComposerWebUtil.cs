using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Appson.Composer.Utility;
using Appson.Composer.Web.Configuration;
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
			get { return GetComponentContext(HttpContext.Current.Application); }
			private set { SetComponentContext(value, HttpContext.Current.Application); }
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

			var configuration = LoadConfiguration();

			if (configuration.RegisterDefaultComponents)
				RegisterDefaultComponents(composer);

			if (configuration.SetResolver)
				SetResolver(composer);

			foreach (SetupCompositionXml xml in configuration.SetupCompositionXmls)
				RunCompositionXml(composer, xml.AssemblyName, xml.ManifestResourceName, xml.Path);
		}

		#endregion

		#region Private helper methods

		private static CompositionConfiguration LoadConfiguration()
		{
			var configuration = (CompositionConfiguration) ConfigurationManager.GetSection("composition");
			return configuration;
		}

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

		private static void RunCompositionXml(ComponentContext composer, string assemblyName,
		                                      string manifestResourceName, string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				composer.ProcessCompositionXml(HostingEnvironment.MapPath(path));
				return;
			}

			var assembly = Assembly.Load(assemblyName);
			composer.ProcessCompositionXmlFromResource(assembly, manifestResourceName);
		}

		#endregion
	}
}