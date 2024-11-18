using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShippingId { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        public Shipping Shipping { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
