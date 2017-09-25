using System.Configuration;
using System.Reflection;
using Appson.Composer.Configuration;

namespace Appson.Composer.Utility
{
    public static class ComposerConfigurationUtil
    {
        public static void ProcessApplicationConfiguration(this IComponentContext composer)
        {
            var configuration = LoadConfiguration();
            if (configuration?.SetupCompositionXmls == null)
                return;

            foreach (SetupCompositionXml xml in configuration.SetupCompositionXmls)
                RunCompositionXml(composer, xml.AssemblyName, xml.ManifestResourceName, xml.Path);
        }

        #region Private helper methods

        private static CompositionConfiguration LoadConfiguration()
        {
            return ConfigurationManager.GetSection("composition") as CompositionConfiguration;
        }

        private static void RunCompositionXml(IComponentContext composer, string assemblyName,
            string manifestResourceName, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                composer.ProcessCompositionXml(path);
                return;
            }

            var assembly = Assembly.Load(assemblyName);
            composer.ProcessCompositionXmlFromResource(assembly, manifestResourceName);
        }

        #endregion

    }
}