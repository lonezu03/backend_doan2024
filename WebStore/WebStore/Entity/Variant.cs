using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
  
    public class Variant
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int? Color_Id { get; set; }
        public int? Size_Id { get; set; }
        public int? Description_Id { get; set; }
        public string? Image { get; set; }
        public int? Category_Id { get; set; }


        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Category Category { get; set; }
        public Description Description { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

    }
}
