namespace WebStore.Entity
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }

        public Order Order { get; set; }
        public Inventory Inventory { get; set; }
    }
}
