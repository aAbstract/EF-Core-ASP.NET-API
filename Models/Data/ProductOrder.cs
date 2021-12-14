namespace DB_API_TEST.Models.Data
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        
        public int Quantity { get; set; }

        // foreign key field
        public int PrductId;
        // foreign key object
        public Product Product { get; set; }

        // foreign key field
        public int OrderId;
        // foreign key object
        public Order Order { get; set; }
    }
}