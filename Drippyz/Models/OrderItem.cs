using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    public class OrderItem
    {
        [key]
        public int Id { get; set; }

        public int Total { get; set; }

        public double Price { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        //relationship Orders
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        public Order Order { get; set; }

    }
}
