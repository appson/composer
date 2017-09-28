using Appson.Composer;

namespace QuickStartTwo
{
    class Program
    {
        static void Main()
        {
            var composer = new ComponentContext();
            composer.Register(typeof(ConsoleLogger));
            composer.Register(typeof(DefaultCustomerData));
            composer.Register(typeof(DefaultOrderData));
            composer.Register(typeof(DefaultOrderLogic));

            composer.GetComponent<IOrderLogic>().PlaceOrder("John", 17);
        }
    }
}
