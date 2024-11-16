namespace Process1.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public void CalculateSubtotal()
        {
            Subtotal = Product.Price * Quantity;
        }
    }
}
