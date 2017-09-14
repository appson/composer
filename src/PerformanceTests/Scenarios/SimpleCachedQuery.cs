using System;
using System.Diagnostics;
using Appson.Composer.PerformanceTests.Components;

namespace Appson.Composer.PerformanceTests.Scenarios
{
    public class SimpleCachedQuery : ITestScenario
    {
        public void Run()
        {
            var composer = new ComponentContext();
            composer.Register(typeof(CachedComponentOne));
            composer.GetComponent<ICachedComponentOne>();

            Console.WriteLine("Querying a cached component from composer for 500,000 times...");

            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 500000; i++)
            {
                var temp = composer.GetComponent<ICachedComponentOne>();
            }

            sw.Stop();
            Console.WriteLine($"    - Done in {sw.ElapsedMilliseconds:N} milliseconds");
        }
    }
}