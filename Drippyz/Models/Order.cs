using System.ComponentModel.DataAnnotations;

namespace Drippyz.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string UserId { get; set; }

        //Relationship of Order with Order Item 
        public List<Item> Items { get; set; }
    }
}
