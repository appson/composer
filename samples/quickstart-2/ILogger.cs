using Appson.Composer;

namespace QuickStartTwo
{
    [Contract]
    public interface ILogger
    {
        void Log(string log);
    }
}