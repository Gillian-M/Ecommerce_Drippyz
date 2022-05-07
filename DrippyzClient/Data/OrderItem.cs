using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    public class OrderItem : Product
    {
        
        public int Id { get; set; }

        public int Total { get; set; }

        public double Price { get; set; }

        public int ProductId { get; set; }
        
        public int OrderId { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0} | Total: {1} | Price: {2} | Product Id: {3} | Order Id: {4}", Id, Total, Price, ProductId, OrderId);

        }

    }
}
