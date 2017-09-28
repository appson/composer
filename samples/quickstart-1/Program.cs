using System;
using Appson.Composer;

namespace QuickStartOne
{
    class Program
    {
        static void Main()
        {
            var composer = new ComponentContext();
            composer.Register(typeof(DefaultLogger));

            composer.GetComponent<ILogger>().Log("Hello, compositional world!");
        }
    }

    [Contract]
    public interface ILogger
    {
        void Log(string log);
    }

    [Component]
    public class DefaultLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}
