using WebStore.Entity;

namespace WebStore.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Material_Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal price { get; set; }
        public int Gender_Id { get; set; }
        public string? Image { get; set; }


        // public Material Material { get; set; }
        //  public Gender Gender { get; set; }
        // public ICollection<Variant> Variants { get; set; }
    }
}
