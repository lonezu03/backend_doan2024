namespace WebStore.DTO
{
    public class VariantDto
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Color_Id { get; set; }
        public int Size_Id { get; set; }
        public int? Description_Id { get; set; }
        public int Category_Id { get; set; }
        public string? Image { get; set; }
    }
}
