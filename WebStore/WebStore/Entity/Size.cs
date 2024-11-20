using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
   
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Variant> Variants { get; set; }

    }
}
