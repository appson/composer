using Appson.Composer.Cache;

namespace Appson.Composer.UnitTests.ComponentInstantiations.Components
{
    [Contract]
    [Component]
    [ComponentCache(typeof(ThreadLocalComponentCache))]
    public class ThreadLocalComponent : ISomeContract, IAnotherContract
    {
        
    }
}