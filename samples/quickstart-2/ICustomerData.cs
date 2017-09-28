using Appson.Composer;

namespace QuickStartTwo
{
    [Contract]
    public interface ICustomerData
    {
        int GetCustomerId(string customerName);
    }
}