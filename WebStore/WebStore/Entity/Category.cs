using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{
    
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
