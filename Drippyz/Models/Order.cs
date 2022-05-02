using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        //Relationship of Order with Order Item 
        public List<OrderItem> OrderItems { get; set; }
    }
}
