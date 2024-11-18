using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Shipping")]
    public class Shipping
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public DateTime ShippingDate { get; set; }

        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
