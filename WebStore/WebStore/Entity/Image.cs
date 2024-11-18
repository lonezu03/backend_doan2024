using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int VariantId { get; set; }

        public Variant Variant { get; set; }

    }
}
