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
        public int? Category_Id { get; set; }

        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
