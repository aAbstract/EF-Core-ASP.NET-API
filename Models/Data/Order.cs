namespace DB_API_TEST.Models.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public DateTime OrderPlaced { get; set; }
        
        public DateTime? OrderFullfilled { get; set; }
        
        // foreign key field
        public int CustomerId { get; set; }
        // foreign key object
        public Customer Customer { get; set; }

        // one to many relation (Order -> Product)
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}