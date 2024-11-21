namespace WebStore.Entity
{
    public class Description
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Variant> Variant { get; set; }


    }

}
