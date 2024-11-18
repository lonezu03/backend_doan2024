using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Entity;

namespace WebStore.Entity
{
    [Table("Product")]

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Material_Id { get; set; }
        public int Gender_Id { get; set; }

        public Material Material { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Variant> Variants { get; set; }

    }
}
