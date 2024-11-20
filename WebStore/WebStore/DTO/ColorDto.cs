using WebStore.Entity;

namespace WebStore.DTO
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Variant> Variants { get; set; }
    }
}
