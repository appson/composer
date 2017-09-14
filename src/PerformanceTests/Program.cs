using System.Collections.Generic;
using Appson.Composer.PerformanceTests.Scenarios;

namespace Appson.Composer.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var scenarios = BuildScenarioList();
            foreach (var scenario in scenarios)
            {
                scenario.Run();
            }
        }

        private static IEnumerable<ITestScenario> BuildScenarioList()
        {
            return new ITestScenario[]
            {
                new SimpleCachedQuery(),
                new SimpleUncachedQuery(), 
                new PropertyInjection(), 
                new ConstructorInjection(), 
                new ArrayOfSimilarlyNamed(), 
                new ArrayOfDefferentlyNamed(), 
                new OpenGeneric(), 
                new RegisterAndResolve(), 
                new PrepareContext(), 
                new PrepareContextAndRegister(), 
                new PrepareContextAndRegisterAndResolve()
            };
        }
    }
}
