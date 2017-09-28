using Appson.Composer;

namespace QuickStartTwo
{
    [Contract]
    public interface IOrderData
    {
        void SaveOrderData(string description);
    }
}