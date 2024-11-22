using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Entity
{

    public class Address
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Address_Line { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Users User { get; set; }
    }
}
