using System;
using System.Diagnostics;
using Appson.Composer.PerformanceTests.Components;

namespace Appson.Composer.PerformanceTests.Scenarios
{
    public class SimpleUncachedQuery : ITestScenario
    {
        public void Run()
        {
            var composer = new ComponentContext();
            composer.Register(typeof(UncachedComponentOne));
            composer.GetComponent<IUncachedComponentOne>();

            Console.WriteLine("Querying an uncached component from composer for 500,000 times...");

            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 500000; i++)
            {
                var temp = composer.GetComponent<IUncachedComponentOne>();
            }

            sw.Stop();
            Console.WriteLine($"    - Done in {sw.ElapsedMilliseconds:N} milliseconds");
        }
    }
}