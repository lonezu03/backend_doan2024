namespace WebStore.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? InventoryId { get; set; }
        public int? variant_id { get; set; }
        public string status { get; set; }
        public int quantity { get; set; }
        public string? imagesp { get; set; }
        public string? color { get; set; }
        public string? size { get; set; }
        public string? name { get; set; }
        public decimal? price { get; set; }


    }

}
