using System;
using Appson.Composer;

namespace QuickStartTwo
{
    [Component]
    public class ConsoleLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}