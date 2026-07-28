namespace ECommerceApi.Models
{
#pragma warning disable CS1591
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
