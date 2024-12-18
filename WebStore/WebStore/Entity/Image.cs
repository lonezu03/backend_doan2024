using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Variant_Id { get; set; }


    }
}
