using Appson.Composer;

namespace QuickStartTwo
{
    [Component]
    public class DefaultOrderLogic : IOrderLogic
    {
        [ComponentPlug] public IOrderData OrderData { get; set; }
        [ComponentPlug] public ICustomerData CustomerData { get; set; }
        [ComponentPlug] public ILogger Logger { get; set; }

        public void PlaceOrder(string customerName, int amount)
        {
            Logger.Log($"Placing order for {customerName} with the amount = {amount}");

            var customerId = CustomerData.GetCustomerId(customerName);
            OrderData.SaveOrderData($"Order for customer {customerId}: {amount} items");

            Logger.Log("Done.");
        }
    }
}