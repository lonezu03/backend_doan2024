using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{

    public class Orders
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Shipping_Id { get; set; }
        public DateTime Date { get; set; }

        //public User User { get; set; }
        //public Shipping Shipping { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; }// 1-n
    }
}
