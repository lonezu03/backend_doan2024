using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Variant")]
    public class Variant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int? CategoryId { get; set; }

        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
