namespace DB_API_TEST.Models.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }
        
        // one to many relation (Customer -> Order)
        public ICollection<Order> Orders { get; set; }
    }
}