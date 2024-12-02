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

    }

}
