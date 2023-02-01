namespace EcommerceAPI.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
