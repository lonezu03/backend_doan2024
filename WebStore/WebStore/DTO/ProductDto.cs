using WebStore.Entity;

namespace WebStore.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Material_Id { get; set; }
        public int Gender_Id { get; set; }

      // public Material Material { get; set; }
      //  public Gender Gender { get; set; }
       // public ICollection<Variant> Variants { get; set; }
    }
}
