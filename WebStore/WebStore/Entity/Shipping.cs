using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    
    public class Shipping
    {
        public int Id { get; set; }
        public int Address_Id { get; set; }
        public DateTime Shipping_Date { get; set; }

        public Address Address { get; set; }
        public ICollection<Orders> Orders { get; set; }// 1-n với Order
    }
}
