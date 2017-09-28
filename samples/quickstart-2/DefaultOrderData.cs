using Appson.Composer;

namespace QuickStartTwo
{
    [Component]
    public class DefaultOrderData : IOrderData
    {
        [ComponentPlug]
        public ILogger Logger { get; set; }

        public void SaveOrderData(string description)
        {
            Logger.Log($"Saving order: {description}");    
        }
    }
}