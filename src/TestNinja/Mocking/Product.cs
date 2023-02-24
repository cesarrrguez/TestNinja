namespace TestNinja.Mocking
{
    public class Product
    {
        public float ListPrice { get; set; }

        public float GetPrice(ICustomer customer)
        {
            return customer.IsGold
                ? ListPrice * 0.7f
                : ListPrice;
        }
    }

    public interface ICustomer
    {
        bool IsGold { get; set; }
    }

    public class Customer : ICustomer
    {
        public bool IsGold { get; set; }
    }
}