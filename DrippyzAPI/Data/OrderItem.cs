using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Total { get; set; }

        public double Price { get; set; }

        public int ProductId { get; set; }
        
        public int OrderId { get; set; }
        
    }
}
