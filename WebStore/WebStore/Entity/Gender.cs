using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    [Table("Gender")]

    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Product> Products { get; set; }



    }
}
