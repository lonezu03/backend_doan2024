using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
   
    public class Inventory
    {
        public int Id { get; set; }
        public int Variant_Id { get; set; }
        public int Quantity { get; set; }

        public Variant Variant { get; set; }
        public Order_Item Order_Item { get; set; }
    }
}
