using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    public class Order_Item
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Inventory_Id { get; set; }

        public Orders Order { get; set; } // n-1 với Order
        public Inventory Inventory { get; set; }
    }
}
