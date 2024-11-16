namespace Process1.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public void AddProduct(Product product, int quantity)
        {
            var orderItem = new OrderItem
            {
                ProductID = product.ProductID,
                Quantity = quantity,
                Subtotal = product.Price * quantity
            };
            OrderItems.Add(orderItem);
        }

        public void RemoveProduct(int productId)
        {
            var item = OrderItems.FirstOrDefault(i => i.ProductID == productId);
            if (item != null)
            {
                OrderItems.Remove(item);
            }
        }

        public decimal CalculateTotal()
        {
            return OrderItems.Sum(item => item.Subtotal);
        }
    }
}