namespace Drippyz.Models
{
    public class ShoppingCartItem
    {
        [key]
        public int Id { get; set; }
        public Product Product  { get; set; }
        public int Total { get; set; }

        //Shopping cart relationship with Item 
        public string ShoppingCartId { get; set; }
    }
}
