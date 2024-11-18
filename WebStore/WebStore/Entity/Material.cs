using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Material")]

    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Product> Products { get; set; }


    }
}
