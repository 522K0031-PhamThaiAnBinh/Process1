namespace Process1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public object GetDetails()
        {
            return new
            {
                ProductID,
                Name,
                Price
            };
        }
    }
}