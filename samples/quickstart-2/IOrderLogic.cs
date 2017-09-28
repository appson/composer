using Appson.Composer;

namespace QuickStartTwo
{
    [Contract]
    public interface IOrderLogic
    {
        void PlaceOrder(string customerName, int amount);
    }
}