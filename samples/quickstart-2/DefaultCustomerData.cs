using Appson.Composer;

namespace QuickStartTwo
{
    [Component]
    public class DefaultCustomerData : ICustomerData
    {
        [ComponentPlug]
        public ILogger Logger { get; set; }

        public int GetCustomerId(string customerName)
        {
            Logger.Log($"Looking up customer with name {customerName}...");
            return 5;
        }
    }
}