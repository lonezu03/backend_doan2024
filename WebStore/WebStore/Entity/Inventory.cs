using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Inventory")]
    public class Inventory
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }

        public Variant Variant { get; set; }
    }
}
