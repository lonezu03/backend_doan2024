using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Color")]
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Variant> Variants { get; set; }

    }
}
